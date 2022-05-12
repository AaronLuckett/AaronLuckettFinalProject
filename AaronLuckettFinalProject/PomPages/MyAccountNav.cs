using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AaronLuckettFinalProject.PomPages
{
    public class MyAccountNav
    {
        private IWebDriver driver;

        /*
         * Constructor for class
         */
        public MyAccountNav(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        By logoutButtonLocator = By.LinkText("Logout");

        //Elements
        IWebElement emailBox => driver.FindElement(By.Name("username"));
        IWebElement passwordBox => driver.FindElement(By.Name("password"));
        IWebElement shopButton => driver.FindElement(By.LinkText("Shop"));
        IWebElement loginButton => driver.FindElement(By.Name("login"));
        IWebElement orders => driver.FindElement(By.LinkText("Orders"));
        IWebElement logoutButton => driver.FindElement(logoutButtonLocator);
        IWebElement cartButton => driver.FindElement(By.LinkText("Cart"));


        //Methods
        /*
         * Will enter login details to login to shop
         */
        public void Login(String username, String password)
        {
            //Find the username entry box and enters email
            emailBox.SendKeys(username);
            Console.WriteLine("Email entered");

            //Find the password entry box and enters password
            passwordBox.SendKeys(password);
            Console.WriteLine("Password entered");

            //Click login button
            loginButton.Click();
        }


        /*
         * Method which clicks on shop button
         */
        public void GoShop()
        {
            shopButton.Click();
        }


        /*
         * Method which goes to past orders
         */
        public void GoToOrders()
        {
            orders.Click();
        }


        /*
         * Method to logout
         */
        public void Logout()
        {
            //Will check for stale error and confirm button is still present
            IWebElement logout = driver.FindElement(logoutButtonLocator);
            try
            {
                logout.Click();
            }
            catch (StaleElementReferenceException e)
            {
                //If fails find button again and then click on it
                logout = driver.FindElement(logoutButtonLocator);
                logoutButton.Click();
            }
        }


        /*
         * Method to go to cart
         */
        public void GoToCart()
        {
            cartButton.Click();
        }
    }
}
