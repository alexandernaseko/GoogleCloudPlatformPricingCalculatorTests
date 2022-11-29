using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudPlatformPricingCalculatorTests.PageObjects
{
    internal class MainMenuPageObject
    {
        private IWebDriver driver;

        private const string SEARCH_TEXT = "Google Cloud Platform Pricing Calculator";

        private readonly By googleSearchField = By.XPath("//input[@class='devsite-search-field devsite-search-query']");

        public MainMenuPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public SearchResultsPageObject searchGCloudCalculator()
        {
            WaitUntil.WaitElement(driver, googleSearchField, 10);

            //search gcalc
            WebElement search = (WebElement)driver.FindElement(googleSearchField);
            search.Click();
            search.SendKeys(SEARCH_TEXT + Keys.Enter);

            return new SearchResultsPageObject(driver);
        }
    }
}
