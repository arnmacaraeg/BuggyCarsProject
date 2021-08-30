using OpenQA.Selenium;
using BuggyCars.Model.Helpers;

namespace BuggyCars.Model.Pages
{
    public class LoginPage : BasePage
    {
        public IWebElement UsernameField => driver.FindElement(By.Name("login"));
        public IWebElement PasswordField => driver.FindElement(By.Name("password"));
        public IWebElement SubmitButton => driver.FindElement(By.XPath("//button[@type='submit'][text()='Login']"));
        public IWebElement PopularMakeLink => driver.FindElement(By.XPath("//a[contains(@href,'/make/')]"));


        public LoginPage(IWebDriver driver) : base(driver)
        {

        }

        public void SetLoginUser(string user)
        {
            waitHelper.WaitForElementClickable(UsernameField);
            UsernameField.SendKeys(user);
        }

        public void SetLoginPassword(string password)
        {
            waitHelper.WaitForElementClickable(UsernameField);
            PasswordField.SendKeys(password);
        }
    }
}
