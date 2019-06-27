using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Protractor;
using OpenQA.Selenium.Support.UI;

namespace Payquest_Testing
{

    [TestFixture]

    public class Class1
    {
        public IWebDriver driver;
        public NgWebDriver ngDriver;

        public string BaseURL
        {
            get
            {
                return ConfigurationManager.AppSettings["url"];
            }
        }


        public string DriverPath { get { return ConfigurationManager.AppSettings["ChromeDriverPath"]; } }

        public void Open(string url)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            driver = new ChromeDriver(DriverPath, options);

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(5);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromMinutes(5);

            ngDriver = new NgWebDriver(driver)
            {
                Url = url
            };
            ngDriver.WaitForAngular();

        }


        public void Close()
        {
            driver.Close();
            driver.Quit();
        }



        public void RefreshPage()
        {
            if (driver != null)
            {
                driver.Navigate().Refresh();
            }
        }

        public void ClickTab(string dataTarget)
        {
            var targetXpath = string.Format("//li[a/@data-target='{0}']", dataTarget);

            var element = driver.FindElement(By.XPath(targetXpath));
            Assert.IsNotNull(element, string.Format("DataTarget : {0}", dataTarget));
            element.Click();

        }


        public NgWebElement GetElement(string elementID)
        {
            NgWebElement element = ngDriver.FindElement(By.Id(elementID));
            Assert.IsNotNull(element, string.Format("ElementID : {0}", elementID));
            return element;

        }



        public void ClickElement(IWebElement element)
        {
            // ScrollToView(element);
            element.Click();
            ngDriver.WaitForAngular();
        }

        public void Save()
        {
            var save = driver.FindElement(By.Id(string.Format("submit")));
            save.Click();

        }



        public void SetElementValue(NgWebElement element, string newValue)
        {

            // element.Clear();
            element.SendKeys(newValue);

            ngDriver.WaitForAngular();
        }

        public string GetElementValue(string elementID, int maxSeconds)
        {
            NgWebElement element = ngDriver.FindElement(By.Id(elementID));
            int waitCount = 0;

            var actual = element.GetAttribute("value");



            while (!element.Displayed && ++waitCount <= maxSeconds)
            {
                System.Threading.Thread.Sleep(5000);
                element = GetElement(elementID);
            }

            return actual;
        }


        public void SetNewElement(NgWebElement element, string newValue)
        {

            element.Clear();
            element.SendKeys(newValue);
            ngDriver.WaitForAngular();
        }

        public void SetElementByClick(IWebElement driverelement)
        {

            driverelement.Click();
            new SelectElement(driverelement).SelectByIndex(1);

        }

        public bool WaitForElementToBeDisplayed(string elementID, int maxSeconds)
        {
            var element = GetElement(elementID);
            int waitCount = 0;
            element.Click();

            while (!element.Displayed && ++waitCount <= maxSeconds)
            {
                System.Threading.Thread.Sleep(5000);
                element = GetElement(elementID);
            }
            return element.Displayed;
        }

        public NgWebElement GetElementByXPath(string xpathQuery)
        {
            NgWebElement element = ngDriver.FindElement(By.XPath(xpathQuery));
            Assert.IsNotNull(element, string.Format("Query : {0}", xpathQuery));
            return element;
        }

        public bool WaitForElementXpathToBeDisplayed(string elementXpath, int maxSeconds)
        {
            var element = GetElementByXPath(elementXpath);
            int waitCount = 0;



            while (!element.Displayed && ++waitCount <= maxSeconds)
            {
                System.Threading.Thread.Sleep(5000);
                element = GetElementByXPath(elementXpath);
            }
            return element.Displayed;


        }

        public NgWebElement GetElementByID(string elementbyID)
        {
            ngDriver.WaitForAngular();
            NgWebElement element = ngDriver.FindElement(By.Id(elementbyID));
            return element;


        }


        public void SetSelectedOption(IWebElement element, string text)
        {
            new SelectElement(element).SelectByText(text);
            ngDriver.WaitForAngular();
        }

        public NgWebElement GetPanel(string panelID)
        {
            return ngDriver.FindElement(By.XPath(string.Format("//div[@collapsible-panel='{0}']", panelID)));
        }

        public void ExpandPanel(string panelID)
        {
            NgWebElement header = GetPanel(panelID);
            Assert.IsNotNull(header, string.Format("Collapsible Panel Header: {0}", panelID));

            // Check if the panel is already expanded
            NgWebElement panel = GetElement(panelID);
            Assert.IsNotNull(panel, string.Format("Collapsible Panel : {0}", panelID));

        }



    }
}
