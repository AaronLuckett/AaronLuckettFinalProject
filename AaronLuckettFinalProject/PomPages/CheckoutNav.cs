using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static AaronLuckettFinalProject.Utilities.Helper;

namespace AaronLuckettFinalProject.PomPages
{
    public class CheckoutNav
    {
        IWebDriver driver;

        /*
         * Constructor for class
         */
        public CheckoutNav(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        By placeOrderLocator = By.CssSelector("button#place_order");
        By orderNumberLocator = By.CssSelector(".order > strong");
        By checkPaymentsLocator = By.CssSelector(".payment_method_cheque.wc_payment_method > label");

        //Elements
        IWebElement PlaceOrder => driver.FindElement(placeOrderLocator);
        IWebElement OrderNumber => driver.FindElement(orderNumberLocator);
        IWebElement CheckPayments => driver.FindElement(checkPaymentsLocator);
        IWebElement FirstName => driver.FindElement(By.CssSelector("input#billing_first_name"));
        IWebElement SecondName => driver.FindElement(By.CssSelector("input#billing_last_name"));
        IWebElement StreetAddress => driver.FindElement(By.CssSelector("input[name='billing_address_1']"));
        IWebElement City => driver.FindElement(By.CssSelector("input#billing_city"));
        IWebElement Postcode => driver.FindElement(By.CssSelector("input#billing_postcode"));
        IWebElement PhoneNumber => driver.FindElement(By.CssSelector("input#billing_phone"));
        IWebElement MyAccount => driver.FindElement(By.LinkText("My account"));


        //Methods
        /*
         * Method to enter all details
         */
        public void EnterUserDetails(String first, String second, String home, String city2, String postcode2, string numberPhone)
        {
            ClearPreFilledFields();
            EnterFirstName(first);
            EnterSecondName(second);
            EnterAddress(home);
            EnterCity(city2);
            EnterPostCode(postcode2);
            EnterPhoneNumber(numberPhone);
        }

        /*
         * Private method to clear billing details if fields have been auto filled
         */
        private void ClearPreFilledFields()
        {
            FirstName.Clear();
            SecondName.Clear();
            StreetAddress.Clear();
            City.Clear();
            Postcode.Clear();
            PhoneNumber.Clear();
        }

        /*
         * Method to input first name
         */
        public void EnterFirstName(String name)
        {
            FirstName.SendKeys(name);
        }


        /*
         * Method to enter second name
         */
        public void EnterSecondName(String name)
        {
            SecondName.SendKeys(name);
        }


        /*
         * Method to enter address
         */
        public void EnterAddress(String address)
        {
            StreetAddress.SendKeys(address);
        }


        /*
         * Method to enter city
         */
        public void EnterCity(String city)
        {
            City.SendKeys(city);
        }


        /*
         * Method to enter postcode
         */
        public void EnterPostCode(String postcode)
        {
            Postcode.SendKeys(postcode);
        }


        /*
         * Method to enter phone number
         */
        public void EnterPhoneNumber(String phonenumber)
        {
            PhoneNumber.SendKeys(phonenumber);
        }


        /*
         * Method to confirm order and click button
         */
        public void EnterOrder()
        {
            //Will check for stale error and confirm button is still present
            IWebElement Place = driver.FindElement(placeOrderLocator);
            try
            {
                Place.Click();
            }
            catch (StaleElementReferenceException e)
            {
                //If fails find button again and then click on it
                Place = driver.FindElement(placeOrderLocator);
                PlaceOrder.Click();
            }
        }


        /*
         * Method to return the order number
         */
        public int ReturnOrderNumber()
        {
            WaitForElementToDisplay(orderNumberLocator, 10, driver);
            return Convert.ToInt32(OrderNumber.Text);
        }


        /*
         * Method to take user to their account
         */
        public void GoToMyAccount()
        {
            MyAccount.Click();
        }

        /*
         * Method to click check payment button
         */
        public void ClickCheckPayments()
        {
            //Will check for stale error and confirm button is still present
            IWebElement Check = driver.FindElement(checkPaymentsLocator);
            try
            {
                Check.Click();
            }
            catch (StaleElementReferenceException e)
            {
                //If fails find button again and then click on it
                Check = driver.FindElement(checkPaymentsLocator);
                CheckPayments.Click();
            }
        }

    }
}
