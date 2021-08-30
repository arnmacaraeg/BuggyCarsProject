using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using BuggyCars.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuggyCars.Model.Pages
{
    class ProfilePage : BasePage
    {
        public IWebElement FirstNameField => driver.FindElement(By.Id("firstName"));
        public IWebElement LastNameField => driver.FindElement(By.Id("lastName"));
        public IWebElement GenderField => driver.FindElement(By.Id("gender"));
        public IWebElement AgeField => driver.FindElement(By.Id("age"));
        public IWebElement AddressField => driver.FindElement(By.Id("address"));
        public IWebElement PhoneField => driver.FindElement(By.Id("phone"));
        public IWebElement HobbyDropdownField => driver.FindElement(By.Id("hobby"));
        public IWebElement CurrentPasswordField => driver.FindElement(By.Id("currentPassword"));
        public IWebElement NewPasswordField => driver.FindElement(By.Id("newPassword"));
        public IWebElement ConfirmPasswordField => driver.FindElement(By.Id("newPasswordConfirmation"));
        public IWebElement SaveButton => driver.FindElement(By.XPath("//button[@type='submit'][text()='Save']"));
        public IWebElement ProfileSaveSuccessMessage => driver.FindElement(By.XPath("//div[contains(text(),'The profile has been saved successful')]"));



        public ProfilePage(IWebDriver driver) : base(driver)
        {
            
        }

        public void SetHobby(string hobby)
        {
            var drpdownField = new SelectElement(HobbyDropdownField);
            drpdownField.SelectByText(hobby);
        }

        public void SetGender(string gender)
        {
            GenderField.SendKeys(gender);
            GenderField.SendKeys(Keys.Tab);
        }

        public ProfileData UpdateProfileRandom()
        {
            ProfileData data = new ProfileData();
            var drpdownField = new SelectElement(HobbyDropdownField);

            GenderField.SendKeys("Female");
            GenderField.SendKeys(Keys.Tab);
            AgeField.SendKeys("25");
            AddressField.SendKeys("123 Greenfield Avenue");
            PhoneField.SendKeys("345643665");            
            drpdownField.SelectByText("Video Games");
 
            SaveButton.Click();

            return data.WithFirstName(FirstNameField.GetAttribute("value")).WithLastName(LastNameField.GetAttribute("value")).
                WithGender("Female").WithAge("25").WithAddress("123 Greenfield Avenue").WithPhone("345643665").WithHobby("Video Games");
        }

        public ProfileData GetCurrentProfileInformation()
        {
            ProfileData data = new ProfileData();

            return data.WithFirstName(FirstNameField.GetAttribute("value")).WithLastName(LastNameField.GetAttribute("value"))
                .WithGender(GenderField.GetAttribute("value")).WithAge(AgeField.GetAttribute("value")).WithAddress(AddressField.GetAttribute("value"))
                .WithPhone(PhoneField.GetAttribute("value")).WithHobby(HobbyDropdownField.GetAttribute("value"));
        }
    }
}
