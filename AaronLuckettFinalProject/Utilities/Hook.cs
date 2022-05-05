using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AaronLuckettFinalProject.Utilities
{
    public class Hook
    {
        public IWebDriver driver;

        /*
         * Setup method to be run before each test
         */
        [BeforeScenario]
        public void setup()
        {
            driver = new ChromeDriver();
            driver.Url = Environment.GetEnvironmentVariable("Website_URL");

            //Dismisses the demo store message which got in the way
            IWebElement dismiss = driver.FindElement(By.LinkText("Dismiss"));
            dismiss.Click();
        }


        /*
         * Teardown method to be run after each test is complete
         */
        [AfterScenario]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
