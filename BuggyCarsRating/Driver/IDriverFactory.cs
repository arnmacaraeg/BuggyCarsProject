using OpenQA.Selenium;

namespace BuggyCars.Driver
{
    public interface IDriverFactory
    {
        IWebDriver GetDriver();

        DriverOptions GetOptions();

        IDriverFactory SetHeadless(bool isHeadless);
    }
}
