﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AaronLuckettFinalProject.PomPages
{
    internal class MyAccountNav
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
        IWebElement emailBox => driver.FindElement(By.Name("username"));
        IWebElement passwordBox => driver.FindElement(By.Name("password"));
        IWebElement shopButton => driver.FindElement(By.LinkText("Shop"));
        IWebElement loginButton => driver.FindElement(By.Name("login"));
        IWebElement Orders => driver.FindElement(By.LinkText("Orders"));
        IWebElement LogoutButton => driver.FindElement(By.LinkText("Logout"));


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
            Orders.Click();
        }

        /*
         * Method to logout
         */
        public void Logout()
        {
            LogoutButton.Click();
        }
    }
}
