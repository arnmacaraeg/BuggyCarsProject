using OpenQA.Selenium;


namespace BuggyCars.Model.Pages
{
    class RegistrationPage : BasePage
    {
        public IWebElement LoginField => driver.FindElement(By.Id("username"));
        public IWebElement FirstNameField => driver.FindElement(By.Id("firstName"));
        public IWebElement LastNameField => driver.FindElement(By.Id("lastName"));
        public IWebElement PasswordField => driver.FindElement(By.Id("password"));
        public IWebElement ConfirmPasswordField => driver.FindElement(By.Id("confirmPassword"));
        public By Register => By.XPath("//button[@type='submit'][text()='Register']");
        public IWebElement RegisterButton => driver.FindElement(Register);
        public IWebElement RegistrationSuccessMessage => driver.FindElement(By.XPath("//div[contains(text(),'Registration is successful')]"));
        public IWebElement PasswordNotMatchMessage => driver.FindElement(By.XPath("//div[contains(text(),'Passwords do not match')]"));
        public IWebElement PasswordMustHaveNumericMessage => driver.FindElement(By.XPath("//div[contains(text(),'Password must have numeric characters')]"));
        public IWebElement PasswordMustHaveSymbolMessage => driver.FindElement(By.XPath("//div[contains(text(),'Password must have symbol characters')]"));
        public IWebElement PasswordLengthMessage => driver.FindElement(By.XPath("//div[contains(text(),'minimum field size of 6')]"));
        public IWebElement PasswordMustHaveUpperMessage => driver.FindElement(By.XPath("//div[contains(text(),'Password must have uppercase characters')]"));
        public IWebElement PasswordMustHaveLowerMessage => driver.FindElement(By.XPath("//div[contains(text(),'Password must have lowercase characters')]"));

        public RegistrationPage(IWebDriver driver) : base(driver)
        { 
        
        }

        public void RegisterNewUser(string user, string password, string confirmpwd, string first, string last)
        {
            LoginField.SendKeys(user);
            FirstNameField.SendKeys(first);
            LastNameField.SendKeys(last);
            PasswordField.SendKeys(password);
            ConfirmPasswordField.SendKeys(confirmpwd);
        }

        public void RegisterSuccess()
        {
            waitHelper.WaitForElementClickable(Register);
            RegisterButton.Click();
            System.Threading.Thread.Sleep(4000);
        }
    }
}
