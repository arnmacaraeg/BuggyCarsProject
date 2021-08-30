using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace BuggyCars.Driver
{
    public class RemoteDriverFactory : AbstractDriverFactory
    {
        private readonly string browser;
        private readonly string gridUrl;
        private bool headless = true;

        public RemoteDriverFactory(string browser, string gridUrl)
        {
            this.browser = browser;
            this.gridUrl = gridUrl;
        }

        public override DriverOptions GetOptions()
        {
            return FactoryBuilder.GetFactory(browser).SetHeadless(headless).GetOptions();
        }

        protected override IWebDriver BuildDriver()
        {
            return new RemoteWebDriver(new Uri(gridUrl), GetOptions());
        }

        public override IDriverFactory SetHeadless(bool isHeadless)
        {
            this.headless = isHeadless;
            return this;
        }
    }
}