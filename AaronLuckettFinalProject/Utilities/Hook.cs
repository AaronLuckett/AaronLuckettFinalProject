using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AaronLuckettFinalProject.PomPages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace AaronLuckettFinalProject.Utilities
{
    [Binding]
    public class Hook
    {
        public IWebDriver driver;
        private readonly ScenarioContext _scenarioContext;

        public Hook(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        /*
         * Setup method to be run before each test
         */
        [BeforeScenario]
        public void setup()
        {
            //Will decide on what browser to open based on string in runsettings file
            string driverType = Environment.GetEnvironmentVariable("DriverToUse");
            if(driverType == "chrome")
            {
                driver = new ChromeDriver();
            } else if (driverType == "firefox")
            {
                driver = new FirefoxDriver();
            }
            driver.Manage().Window.Maximize();

            //Context injection
            _scenarioContext["webdriver"] = driver;
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
            //Empty cart if any items inside
            //MyAccountNav myAccount = new MyAccountNav(driver);
            //myAccount.GoToCart();

            //Will remove items from cart
            CartNav cart = new CartNav(driver);
            cart.RemoveAllFromCart();
            Thread.Sleep(3000);
            cart.ProceedToMyAccount();

            //Will logout
            MyAccountNav myAccount2 = new MyAccountNav(driver);
            myAccount2.Logout();

            driver.Quit();
        }
    }
}
