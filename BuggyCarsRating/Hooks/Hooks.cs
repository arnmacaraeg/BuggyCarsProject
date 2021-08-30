using BoDi;
using OpenQA.Selenium;
using BuggyCars.Driver;
using System.Configuration;
using TechTalk.SpecFlow;

namespace BuggyCars.StepDefinition
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private readonly IObjectContainer objectContainer;
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenarioContext;
        //protected static OpenQA.Selenium.IWebDriver driver;

        //For context injections
        public Hooks(IObjectContainer objectContainer, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.objectContainer = objectContainer;
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var appSettings = ConfigurationManager.AppSettings;

            var browser = appSettings["browser"];
            var headless = bool.Parse(appSettings["headless"]);
            var URL = appSettings["URL"];

            IWebDriver driver = FactoryBuilder.GetFactory(browser).SetHeadless(headless).GetDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
            objectContainer.RegisterInstanceAs(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            IWebDriver driver = objectContainer.Resolve<IWebDriver>();
            driver.Close();
            driver.Quit();
            driver.Dispose();
            driver = null;
        }

    }
}
