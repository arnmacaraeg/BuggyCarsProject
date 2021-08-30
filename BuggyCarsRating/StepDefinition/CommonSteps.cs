using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using BuggyCars.StepDefinitions;
using BuggyCars.Model.Data;
using BuggyCars.Model.Pages;
using System.Linq;
using System.Configuration;
using TechTalk.SpecFlow;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace BuggyCars.StepDefinitions
{
    [Binding]
    public class CommonSteps : Steps
    {

        protected readonly IWebDriver driver;
        protected IObjectContainer objectContainer;
        protected readonly ScenarioContext scenarioContext;
        protected readonly FeatureContext featureContext;

        public CommonSteps(IWebDriver driver, IObjectContainer objectContainer, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.driver = driver;
            this.objectContainer = objectContainer;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        [Given(@"that I perform site registration and provide:")]
        public void GivenThatIRegisterOnTheSiteUsing(Table table)
        {
            HomePage home = new HomePage(driver);
            System.Threading.Thread.Sleep(1000);
            home.RegisterButton.Click();

            Random rand = new Random(DateTime.Now.Second);
            RandomNameGen.RandomName nameGen = new RandomNameGen.RandomName(rand);
            string first = nameGen.GenerateFirstname();
            string last = nameGen.GenerateLastname();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var username = new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[rand.Next(s.Length)]).ToArray());

            var password = table.Rows.First()["Password"].ToString();
            var confirmpwd = table.Rows.First()["Confirm Password"].ToString();
            objectContainer.RegisterInstanceAs(new LoginData().WithUsername(username).WithPassword(password), "logincredentials");

            RegistrationPage register = new RegistrationPage(driver);
            System.Threading.Thread.Sleep(1000);
            register.RegisterNewUser(username, password, confirmpwd, first, last);
        }

        [Then(@"verify the successful registration of new user and is able to login")]
        public void ThenVerifyRegistrationIsASuccessAndUserIsAbleToLogin()
        {
            RegistrationPage register = new RegistrationPage(driver);
            System.Threading.Thread.Sleep(1000);
            register.RegisterSuccess();
            Assert.IsTrue(register.RegistrationSuccessMessage.Displayed, "Registration was successfully processed.");

            LoginData loginData = objectContainer.Resolve<LoginData>("logincredentials");

            LoginPage login = new LoginPage(driver);
            System.Threading.Thread.Sleep(1000);
            login.SetLoginUser(loginData.Username);
            login.SetLoginPassword(loginData.Password);

            login.SubmitButton.Click();
            login.waitHelper.WaitForPageLoadComplete();

            HomePage homePage = new HomePage(driver);
            System.Threading.Thread.Sleep(1000);
            Assert.IsTrue(homePage.IsHeaderPresent(), "Login was successful.");
            Assert.IsTrue(homePage.PopularMakeLink.Displayed, "User is taken to home page.");
        }

        [Then(@"verify the passwords do not match")]
        public void ThenVerifyPasswordDoNotMatch()
        {
            RegistrationPage register = new RegistrationPage(driver);
            System.Threading.Thread.Sleep(1000);
            Assert.IsTrue(register.PasswordNotMatchMessage.Displayed, "Passwords must match.");
        }

        [Then(@"verify the password have numeric value")]
        public void ThenVerifyPasswordNumeric()
        {
            RegistrationPage register = new RegistrationPage(driver);
            System.Threading.Thread.Sleep(1000);
            register.RegisterSuccess();
            Assert.IsTrue(register.PasswordMustHaveNumericMessage.Displayed, "Passwords must contain numeric value.");
        }

        [Then(@"verify the password have symbol characters")]
        public void ThenVerifyPasswordSymbols()
        {
            RegistrationPage register = new RegistrationPage(driver);
            System.Threading.Thread.Sleep(1000);
            register.RegisterSuccess();
            Assert.IsTrue(register.PasswordMustHaveSymbolMessage.Displayed, "Passwords must contain symbol value.");
        }

        [Then(@"verify the password minimum length")]
        public void ThenVerifyPasswordLength()
        {
            RegistrationPage register = new RegistrationPage(driver);
            System.Threading.Thread.Sleep(1000);
            register.RegisterSuccess();
            Assert.IsTrue(register.PasswordLengthMessage.Displayed, "Passwords must be at least 8 characters.");
        }

        [Then(@"verify the password must have uppercase characters")]
        public void ThenVerifyPasswordUpper()
        {
            RegistrationPage register = new RegistrationPage(driver);
            System.Threading.Thread.Sleep(1000);
            register.RegisterSuccess();
            Assert.IsTrue(register.PasswordMustHaveUpperMessage.Displayed, "Passwords must contain uppercase characters.");
        }

        [Then(@"verify the password must have lowercase characters")]
        public void ThenVerifyPasswordLower()
        {
            RegistrationPage register = new RegistrationPage(driver);
            System.Threading.Thread.Sleep(1000);
            register.RegisterSuccess();
            Assert.IsTrue(register.PasswordMustHaveLowerMessage.Displayed, "Passwords must contain lowercase characters.");
        }

        [Given(@"a user account (.*)")]
        public void GivenAUserAccount(String username)
        {
            LoginPage loginPage = new LoginPage(driver);
            System.Threading.Thread.Sleep(1000);
            loginPage.SetLoginUser(username);
        }

        [Given(@"a password (.*)")]
        public void GivenAPassword(String password)
        {
            LoginPage loginPage = new LoginPage(driver);
            System.Threading.Thread.Sleep(1000);
            loginPage.SetLoginPassword(password);
        }

        [When(@"the user logs in to the application")]
        public void WhenTheUserLogsInToTheApplication()
        {
            LoginPage loginpage = new LoginPage(driver);
            System.Threading.Thread.Sleep(1000);
            loginpage.SubmitButton.Click();
        }

        [Then(@"the logon result should be (.*)")]
        public void ThenTheUserLogonResultIs(String logonresult)
        {
            HomePage homePage = new HomePage(driver);
            System.Threading.Thread.Sleep(1000);

            if (logonresult == "Success")
            {
                Assert.IsTrue(homePage.IsHeaderPresent(), "Login was successful.");
            }
            else
            {
                Assert.IsTrue(homePage.LoginErrorMessage.Displayed, "Login error message displayed");
            }
        }

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var username = appSettings["username"];
            var password = appSettings["password"];


            LoginPage loginPage = new LoginPage(driver);
            System.Threading.Thread.Sleep(1000);
            loginPage.SetLoginUser(username);
            loginPage.SetLoginPassword(password);
            loginPage.SubmitButton.Click();
        }

        [When(@"I click the log off link")]
        public void WhenIClickTheLogOffLink()
        {
            HomePage home = new HomePage(driver);
            System.Threading.Thread.Sleep(1000);
            home.LogoutButton.Click();
        }

        [Then(@"I should be logged off")]
        public void ThenIShouldBeLoggedOff()
        {
            LoginPage login = new LoginPage(driver);
            System.Threading.Thread.Sleep(1000);

            Assert.IsTrue(login.UsernameField.Displayed, "login:usename field is displayed");
            Assert.IsTrue(login.PasswordField.Displayed, "login:password field is displayed");

        }

        [Then(@"I navigate to Popular Make then back to main page successfully")]
        public void ThenINavigateToPopularMakeThenBackToMainPageSuccessfully()
        {
            HomePage home = new HomePage(driver);
            System.Threading.Thread.Sleep(2000);

            var popularmake = home.PopularMakeName.Text;
            home.PopularMakeLink.Click();

            MakePage model = new MakePage(driver);
            System.Threading.Thread.Sleep(2000);
            Assert.IsTrue(popularmake.Contains(model.MakeName.Text), "page navigated to correct model");

            model.BuggyRatingLink.Click();
            Assert.AreEqual("https://buggy.justtestit.org/", driver.Url, "User should be taken back to the main page");
        }

        [Then(@"I navigate to Popular Model then back to main page successfully")]
        public void ThenINavigateToPopularModelThenBackToMainPageSuccessfully()
        {
            HomePage home = new HomePage(driver);
            System.Threading.Thread.Sleep(2000);

            var popularmodel = home.PopularModelName.Text;
            home.PopularModelLink.Click();

            ModelPage model = new ModelPage(driver);
            System.Threading.Thread.Sleep(2000);
            string pageurl = driver.Url;
            Assert.IsTrue(pageurl.Contains("https://buggy.justtestit.org/model"), "User should be taken to the Model page");
            Assert.IsTrue(popularmodel.Contains(model.ModelName.Text), "page navigated to correct model");

            model.BuggyRatingLink.Click();
            Assert.AreEqual("https://buggy.justtestit.org/", driver.Url, "User should be taken back to the main page");
        }

        [Then(@"I navigate to Overall Rating then back to main page successfully")]
        public void ThenINavigateToOverallRatingThenBackToMainPageSuccesfully()
        {
            HomePage home = new HomePage(driver);
            System.Threading.Thread.Sleep(2000);

            home.OverallLink.Click();
            Assert.AreEqual("https://buggy.justtestit.org/overall", driver.Url, "User should be taken to the Overall page");

            OverallPage model = new OverallPage(driver);
            System.Threading.Thread.Sleep(2000);

            home.BuggyRatingLink.Click();
            Assert.AreEqual("https://buggy.justtestit.org/", driver.Url, "User should be taken back to the main page");
        }

        [Given(@"I am a new user")]
        public void GivenThatIAmANewUser()
        {
            HomePage home = new HomePage(driver);
            System.Threading.Thread.Sleep(1000);
            home.RegisterButton.Click();

            Random rand = new Random(DateTime.Now.Second);
            RandomNameGen.RandomName nameGen = new RandomNameGen.RandomName(rand);
            string first = nameGen.GenerateFirstname();
            string last = nameGen.GenerateLastname();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_@.-!$";
            var username = new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[rand.Next(s.Length)]).ToArray());

            var password = "Testing123!";
            var confirmpwd = "Testing123!";
            objectContainer.RegisterInstanceAs(new LoginData().WithUsername(username).WithPassword(password), "logincredentials");

            RegistrationPage register = new RegistrationPage(driver);
            System.Threading.Thread.Sleep(1000);
            register.RegisterNewUser(username, password, confirmpwd, first, last);
            register.RegisterSuccess();

            objectContainer.RegisterInstanceAs(new LoginData().WithUsername(username).WithPassword(password), "creatednewuser");

            LoginPage login = new LoginPage(driver);
            System.Threading.Thread.Sleep(1000);
            login.SetLoginUser(username);
            login.SetLoginPassword(password);

            login.SubmitButton.Click();
            login.waitHelper.WaitForPageLoadComplete();
        }

        [When(@"I update my profile details")]
        public void WhenIUpdateMyProfileDetails()
        {
            HomePage home = new HomePage(driver);
            System.Threading.Thread.Sleep(2000);
            home.ProfileButton.Click();

            ProfilePage profile = new ProfilePage(driver);
            System.Threading.Thread.Sleep(2000);
            ProfileData data;

            var login = objectContainer.Resolve<LoginData>("creatednewuser");

            data = profile.UpdateProfileRandom();
            System.Threading.Thread.Sleep(2000);

            Assert.IsTrue(profile.ProfileSaveSuccessMessage.Displayed, "save successfully displayed");

            objectContainer.RegisterInstanceAs(new LoginData().WithUsername(login.Username).WithPassword(login.Password), "updatednewuser");
            objectContainer.RegisterInstanceAs<ProfileData>(data, "profiledata");
        }

        [Then(@"I verify profile change was saved")]
        public void ThenIVerifyProfileChangeWasSaved()
        {
            HomePage home = new HomePage(driver);
            System.Threading.Thread.Sleep(2000);
            home.LogoutButton.Click();

            var userprofile = objectContainer.Resolve<LoginData>("updatednewuser");

            LoginPage login = new LoginPage(driver);
            System.Threading.Thread.Sleep(2000);
            Assert.IsTrue(login.PopularMakeLink.Displayed, "User is taken to login page.");
            login.SetLoginUser(userprofile.Username);
            login.SetLoginPassword(userprofile.Password);

            login.SubmitButton.Click();

            var profilesaved = objectContainer.Resolve<ProfileData>("profiledata");

            home.ProfileButton.Click();

            ProfilePage profilepage = new ProfilePage(driver);
            System.Threading.Thread.Sleep(2000);

            var profilecurrent = profilepage.GetCurrentProfileInformation();

            Assert.IsTrue(profilesaved.Equals(profilecurrent), "Saved profile and current profile should be the same");
        }

        [When(@"I added a vote to a model")]
        public void WhenIAddedAVoteToAModel()
        {
            HomePage home = new HomePage(driver);
            System.Threading.Thread.Sleep(2000);

            home.BuggyRatingLink.Click();
            System.Threading.Thread.Sleep(2000);
            home.PopularModelLink.Click();

            ModelPage model = new ModelPage(driver);
            System.Threading.Thread.Sleep(2000);
            scenarioContext.Set(model.VoteNumber.Text, "numberofvotes");

            model.CommentField.SendKeys("Test Comment");
            model.VoteButton.Click();
            model.waitHelper.WaitForJQueryToBeInactive();
        }

        [Then(@"check that my vote was added")]
        public void ThenCheckThatMyVoteWasAdded()
        {
            ModelPage model = new ModelPage(driver);
            System.Threading.Thread.Sleep(2000);
            Assert.AreEqual(model.VoteMessageDone.Text, "Thank you for your vote!", "Vote has been added");

            var afternumofvotes = int.Parse(model.VoteNumber.Text);
            var beforenumofvotes = int.Parse(scenarioContext.Get<string>("numberofvotes"));

            Assert.AreEqual(afternumofvotes, beforenumofvotes + 1, "Vote created should be counted");
        }

    }
}