using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AaronLuckettFinalProject.Utilities
{
    public static class Helper
    {
        /**
         * General method that will apply a wait for a particular element to appear on the page
         */
        public static void WaitForElementToDisplay(By locator, int TimeInSeconds, IWebDriver driver)
        {
            //Wait for element to appear
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TimeInSeconds));
            wait.Until(drv => drv.FindElement(locator).Displayed);
        }


        /**
         * General method to take a screenshot of the webpage
         */
        public static void TakesScreenshot(ITakesScreenshot ssdriver, String ScreenshotName)
        {
            String dt = (DateTime.Today.ToString("yyyyMMdd"));

            Screenshot screenshot = ssdriver.GetScreenshot();
            //Works out the relative path of project and saves in the screenshot folder
            string workingDirectory = Environment.CurrentDirectory;
            string relativePath = (Directory.GetParent(workingDirectory).Parent.Parent.FullName);
            screenshot.SaveAsFile(relativePath + "\\Screenshots\\" + ScreenshotName + dt + ".png", ScreenshotImageFormat.Png);


        }
    }
}
