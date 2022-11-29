using OpenQA.Selenium;
using System.Linq;
using System.Text.RegularExpressions;

namespace GoogleCloudPlatformPricingCalculatorTests.PageObjects
{
    class TempMailPageObject
    {
        IWebDriver driver;

        private readonly By randomedTempMailButton = By.XPath("//div[@class='txtlien']//b[text()='Random Email Address']");
        private readonly By generatedMailCopyButton = By.XPath("//button[@id='cprnd']");
        private readonly By checkInboxButton = By.XPath("//button[@class='md but text f24 egenbut']//span[text()='Check Inbox']");
        private readonly By refreshInboxButton = By.XPath("//button[@id='refresh']");
        private readonly By mailPriceForCheck = By.XPath("//h3[contains(text(),'USD')]");

        public TempMailPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public TempMailPageObject generateRandomTempMail()
        {
            //create tab and switch to tempmail page
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Navigate().GoToUrl("https://www.yopmail.com/");

            //random mail button
            WaitUntil.WaitElement(driver, randomedTempMailButton, 10);
            driver.FindElement(randomedTempMailButton).Click();

            //copy generated mail
            WaitUntil.WaitElement(driver, generatedMailCopyButton, 10);
            driver.FindElement(generatedMailCopyButton).Click();

            //return to gcalc page
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.Navigate().Refresh();

            return new TempMailPageObject(driver);
        }

        public TempMailPageObject switchToMailPage()
        {
            //goto tempmail page
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Navigate().Refresh();

            //click check inbox button
            WaitUntil.WaitElement(driver, checkInboxButton, 10);
            driver.FindElement(checkInboxButton).Click();

            //click refresh inbox button
            WaitUntil.WaitElement(driver, refreshInboxButton, 10);
            driver.FindElement(refreshInboxButton).Click();

            //switch fo tempmail iframe
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame("ifmail");
            
            return new TempMailPageObject(driver);
        }

        public double getMailPrice()
        {
            WaitUntil.WaitInterval(2);

            //wait and get price from mail
            WaitUntil.WaitElement(driver, mailPriceForCheck,10);
            
            //out variable for price
            double price;
            
            //parse in string and return price if parse succeeded
            var tryParsePrice = double.TryParse(Regex.Match(driver.FindElement(mailPriceForCheck).Text, @"[0-9]+([,][0-9]+)[\.][0-9]+").Value,out price);
            
            return tryParsePrice ? price : 0.00;
        }
    }
}
