using OpenQA.Selenium;


namespace BuggyCars.Model.Pages
{
    class MakePage : BasePage
    {
        public IWebElement CommentField => driver.FindElement(By.Id("comment"));
        public IWebElement VoteNumber => driver.FindElement(By.CssSelector("div:nth-child(1) > h4 > strong"));
        public IWebElement VoteButton => driver.FindElement(By.XPath("//button[@class='btn btn-success'][text()='Vote!']"));
        public IWebElement VoteMessageDone => driver.FindElement(By.XPath("//p[@class='card-text']"));
        public IWebElement MakeName => driver.FindElement(By.CssSelector("div:nth-child(1) > h3"));


        public MakePage(IWebDriver driver) : base(driver)
        { 
        
        }

    }
}
