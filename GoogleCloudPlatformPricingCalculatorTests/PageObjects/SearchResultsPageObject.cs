using OpenQA.Selenium;

namespace GoogleCloudPlatformPricingCalculatorTests.PageObjects
{
    class SearchResultsPageObject
    {
        private IWebDriver driver;

        private readonly By googleCloudCalculatorText = By.XPath("//b[text()='Google Cloud Pricing Calculator']");

        public SearchResultsPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public GCloudCalculatorPageObject switchToGCloudCalculator()
        {
            //goto gcalc
            WaitUntil.WaitElement(driver, googleCloudCalculatorText, 10);
            driver.FindElement(googleCloudCalculatorText).Click();

            return new GCloudCalculatorPageObject(driver);
        }
    }
}
