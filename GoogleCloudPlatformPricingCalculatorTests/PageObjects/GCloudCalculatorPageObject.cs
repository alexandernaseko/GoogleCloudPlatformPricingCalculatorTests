using OpenQA.Selenium;
using System.Linq;
using System.Text.RegularExpressions;

namespace GoogleCloudPlatformPricingCalculatorTests.PageObjects
{
    class GCloudCalculatorPageObject
    {
        private IWebDriver driver;

        private readonly By iFrame = By.XPath("//iframe[contains(@name,'goog_')]");
        private readonly By iFrame2 = By.XPath("//iframe[contains(@id,'myFrame')]");

        private readonly By numberOfInstances = By.XPath("//input[@ng-model='listingCtrl.computeServer.quantity']");

        private readonly By typeOSCurrent = By.XPath("//md-select[@ng-model='listingCtrl.computeServer.os']");
        private readonly By typeOSChanged = By.XPath("//md-option[@value='free']");

        private readonly By machineFamilyCurrent = By.XPath("//md-select[@ng-model='listingCtrl.computeServer.family']");
        private readonly By machineFamilyChanged = By.XPath("//md-option[@value='gp']");

        private readonly By seriesCurrent = By.XPath("//md-select[@ng-model='listingCtrl.computeServer.series']");
        private readonly By seriesChanged = By.XPath("//md-option[@value='e2']");

        private readonly By machineTypeCurrent = By.XPath("//md-select[@ng-model='listingCtrl.computeServer.instance']");
        private readonly By machineTypeChanged = By.XPath("//md-option[@value='CP-COMPUTEENGINE-VMIMAGE-E2-STANDARD-8']");

        private readonly By addToEstimateButton = By.XPath("//button[@ng-click='listingCtrl.addComputeServer(ComputeEngineForm);']");

        private readonly By numberOfNodes = By.XPath("//input[@ng-model='listingCtrl.soleTenant.nodesCount']");
        private readonly By GPUsCheckBox = By.XPath("//md-checkbox[@ng-model='listingCtrl.soleTenant.addGPUs']");
        private readonly By gpuType = By.XPath("//md-select[@ng-model='listingCtrl.soleTenant.gpuType']");
        private readonly By gpuTypeModel = By.XPath("//md-option[@value='NVIDIA_TESLA_V100']");
        private readonly By numberOfGpusCurrent = By.XPath("//md-select[@ng-model='listingCtrl.soleTenant.gpuCount']");
        private readonly By cPUOvercommitCheckBox = By.XPath("//md-checkbox[@ng-model='listingCtrl.soleTenant.cpuOvercommit']");
        private readonly By localSSDCurrent = By.XPath("//md-select[@ng-model='listingCtrl.soleTenant.ssd']");

        private readonly By dataCenterCurrent = By.XPath("//md-select[@ng-model='listingCtrl.soleTenant.location']");

        private readonly By committedUsageCurrent = By.XPath("//md-select[@ng-model='listingCtrl.soleTenant.cud']");
        private readonly By committedUsageChanged = By.XPath("//md-option[@id='select_option_156']");

        private readonly By addToEstimateButton2 = By.XPath("//form[@name='SoleTenantForm']//button[@class='md-raised md-primary cpc-button md-button md-ink-ripple']");
        
        private readonly By emailEstimateButton = By.XPath("//button[@id='email_quote']");

        private readonly By gCloudTotalPrice = By.XPath("//h2[b[@class='ng-binding']]");
        
        public GCloudCalculatorPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public GCloudCalculatorPageObject fillDataInstancesForm()
        {

            //switch to iframe
            WaitUntil.WaitElement(driver,iFrame,10);
            WebElement frame = (WebElement)driver.FindElement(iFrame);
            driver.SwitchTo().Frame(frame);

            //switch to iframe2
            WaitUntil.WaitElement(driver,iFrame2,10);
            WebElement frame2 = (WebElement)driver.FindElement(iFrame2);
            driver.SwitchTo().Frame(frame2);

            //number of instances
            WaitUntil.WaitElement(driver, numberOfInstances, 10);
            driver.FindElement(numberOfInstances).SendKeys("4");

            //change typeOS
            WaitUntil.WaitElement(driver, typeOSCurrent,10);
            driver.FindElement(typeOSCurrent).Click();
            WaitUntil.WaitElement(driver, typeOSChanged, 10);
            driver.FindElement(typeOSChanged).Click();

            //change machine class
            WaitUntil.WaitElement(driver, machineFamilyCurrent,10);
            driver.FindElement(machineFamilyCurrent).Click();
            WaitUntil.WaitElement(driver, machineFamilyChanged, 10);
            driver.FindElement(machineFamilyChanged).Click();

            //change series
            WaitUntil.WaitElement(driver, seriesCurrent, 10);
            driver.FindElement(seriesCurrent).Click();
            WaitUntil.WaitElement(driver, seriesChanged, 10);
            driver.FindElement(seriesChanged).Click();

            //change machine type
            WaitUntil.WaitElement(driver,machineTypeCurrent,10);
            driver.FindElement(machineTypeCurrent).Click();
            WaitUntil.WaitElement(driver, machineTypeChanged, 10);
            driver.FindElement(machineTypeChanged).Click();

            //add to estimate
            WaitUntil.WaitElement(driver, addToEstimateButton, 10);
            driver.FindElement(addToEstimateButton).Click();

            return new GCloudCalculatorPageObject(driver);
        }

        public GCloudCalculatorPageObject fillDataSoleTenantNodesForm()
        {
            //enter numbers of nodes
            WaitUntil.WaitElement(driver, numberOfNodes, 10);
            driver.FindElement(numberOfNodes).SendKeys("1");

            //set gpus checkbox 
            WaitUntil.WaitElement(driver, GPUsCheckBox, 10);
            driver.FindElement(GPUsCheckBox).Click();

            //set gpu type
            WaitUntil.WaitElement(driver,gpuType,10);
            driver.FindElement(gpuType).Click();
            WaitUntil.WaitElement(driver, gpuTypeModel, 10);
            driver.FindElement(gpuTypeModel).Click();

            //set numbers of gpu
            WaitUntil.WaitElement(driver,numberOfGpusCurrent,10);
            driver.FindElement(numberOfGpusCurrent).Click();
            // ¯\_(ツ)_ /¯
            var numbersGPU = driver.FindElement(numberOfGpusCurrent);
            numbersGPU.SendKeys("8");
            numbersGPU.SendKeys(Keys.Enter);

            // set cpu overcommit check box
            WaitUntil.WaitElement(driver, cPUOvercommitCheckBox, 10);
            driver.FindElement(cPUOvercommitCheckBox).SendKeys(Keys.Enter);

            //set local ssd
            WaitUntil.WaitElement(driver, localSSDCurrent,10);
            //¯\_(ツ)_ /¯
            var ssd = driver.FindElement(localSSDCurrent);
            ssd.SendKeys("24x375 GB");
            ssd.SendKeys(Keys.Enter);

            WaitUntil.WaitInterval(1);

            //set data center
            WaitUntil.WaitElement(driver, dataCenterCurrent, 10);
            //¯\_(ツ)_ /¯
            var data = driver.FindElement(dataCenterCurrent);
            data.SendKeys("Iowa (us-central1)");
            data.SendKeys(Keys.Enter);

            WaitUntil.WaitInterval(1);

            //set commited usage year
            WaitUntil.WaitElement(driver, committedUsageCurrent, 10);
            WebElement el = (WebElement)driver.FindElement(committedUsageChanged);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", el);
            el.SendKeys(Keys.Enter);

            //unused, can remove this
            WaitUntil.WaitInterval(1);
           
            ////add to estimate
            WaitUntil.WaitElement(driver, addToEstimateButton2, 10);
            driver.FindElement(addToEstimateButton2).Click();

            return new GCloudCalculatorPageObject(driver);
        }

        public EmailEstimatePageObject openEmailEstimateForm()
        {
            //return on first tab
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.SwitchTo().DefaultContent();

            //wait and switch to iframe
            WaitUntil.WaitElement(driver, iFrame, 10);
            WebElement frame = (WebElement)driver.FindElement(iFrame);
            driver.SwitchTo().Frame(frame);

            WaitUntil.WaitElement(driver, iFrame2, 10);
            WebElement frame2 = (WebElement)driver.FindElement(iFrame2);
            driver.SwitchTo().Frame(frame2);

            //email button
            WaitUntil.WaitElement(driver, emailEstimateButton, 10);
            driver.FindElement(emailEstimateButton).Click();

            return new EmailEstimatePageObject(driver);
        }

        public double getGCloudPrice()
        {
            //wait for estimated cost
            WaitUntil.WaitElement(driver, gCloudTotalPrice, 10);

            //out variable for price
            double totalPrice;

            //parse in string and return price if parse succeeded
            var tryParsePrice = double.TryParse(Regex.Match(driver.FindElement(gCloudTotalPrice).Text, @"[0-9]+([,][0-9]+)[\.][0-9]+").Value,out totalPrice);
            
            return (tryParsePrice) ? totalPrice : 0.00;
        }
    }
}
