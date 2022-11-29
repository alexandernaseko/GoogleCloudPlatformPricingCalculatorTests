using GoogleCloudPlatformPricingCalculatorTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace GoogleCloudPlatformPricingCalculatorTests
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://cloud.google.com/");
        }

        [Test]
        public void Test1()
        {
            var mainMenu = new MainMenuPageObject(driver);
            var searchResults = new SearchResultsPageObject(driver);
            var gCalculator = new GCloudCalculatorPageObject(driver);
            var tempMail = new TempMailPageObject(driver);
            var emailEstimateForm = new EmailEstimatePageObject(driver);

            mainMenu
                .searchGCloudCalculator();
            searchResults
                .switchToGCloudCalculator();
            gCalculator
                .fillDataInstancesForm()
                .fillDataSoleTenantNodesForm();
            tempMail
                .generateRandomTempMail();
            gCalculator
                .openEmailEstimateForm();
            emailEstimateForm
                .sendEmail();

            var expectedPrice = gCalculator.getGCloudPrice();

            tempMail
                .switchToMailPage();

            var actualPrice = tempMail.getMailPrice();

            Assert.AreEqual(expectedPrice, actualPrice, "Total Estimated Monthly Cost not equals");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
        }
    }
}