using OpenQA.Selenium;
using BuggyCars.Model.Helpers;

namespace BuggyCars.Model.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver driver;
        public WaitHelperTools waitHelper;

        public IWebElement BuggyRatingLink => driver.FindElement(By.XPath("//a[@class='navbar-brand'][text()='Buggy Rating']"));

        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
            waitHelper = new WaitHelperTools(driver);
        }
    }
}
