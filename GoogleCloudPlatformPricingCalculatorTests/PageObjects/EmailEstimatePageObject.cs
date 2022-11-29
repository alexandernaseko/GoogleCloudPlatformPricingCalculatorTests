using OpenQA.Selenium;

namespace GoogleCloudPlatformPricingCalculatorTests.PageObjects
{
    class EmailEstimatePageObject
    {
        IWebDriver driver;

        private readonly By mailFieldCalculator = By.XPath("//input[@ng-model='emailQuote.user.email']");

        private readonly By sendEmailButton = By.XPath("//button[@aria-label='Send Email']");

        public EmailEstimatePageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public GCloudCalculatorPageObject sendEmail()
        {
            //mail field in gcalc
            WaitUntil.WaitElement(driver,mailFieldCalculator,10);
            driver.FindElement(mailFieldCalculator).Click();

            //paste copied mail
            driver.FindElement(mailFieldCalculator).SendKeys(Keys.Control + "v");

            //send mail
            WaitUntil.WaitElement(driver,sendEmailButton,10);
            driver.FindElement(sendEmailButton).Click();
            
            return new GCloudCalculatorPageObject(driver);
        }
    }
}
