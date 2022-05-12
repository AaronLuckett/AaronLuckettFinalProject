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
        IWebElement placeOrder => driver.FindElement(placeOrderLocator);
        IWebElement orderNumber => driver.FindElement(orderNumberLocator);
        IWebElement checkPayments => driver.FindElement(checkPaymentsLocator);
        IWebElement firstName => driver.FindElement(By.CssSelector("input#billing_first_name"));
        IWebElement secondName => driver.FindElement(By.CssSelector("input#billing_last_name"));
        IWebElement streetAddress => driver.FindElement(By.CssSelector("input[name='billing_address_1']"));
        IWebElement city => driver.FindElement(By.CssSelector("input#billing_city"));
        IWebElement postcode => driver.FindElement(By.CssSelector("input#billing_postcode"));
        IWebElement phoneNumber => driver.FindElement(By.CssSelector("input#billing_phone"));
        IWebElement myAccount => driver.FindElement(By.LinkText("My account"));


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
            firstName.Clear();
            secondName.Clear();
            streetAddress.Clear();
            city.Clear();
            postcode.Clear();
            phoneNumber.Clear();
        }

        /*
         * Method to input first name
         */
        public void EnterFirstName(String name)
        {
            firstName.SendKeys(name);
        }


        /*
         * Method to enter second name
         */
        public void EnterSecondName(String name)
        {
            secondName.SendKeys(name);
        }


        /*
         * Method to enter address
         */
        public void EnterAddress(String address)
        {
            streetAddress.SendKeys(address);
        }


        /*
         * Method to enter city
         */
        public void EnterCity(String cityEntered)
        {
            city.SendKeys(cityEntered);
        }


        /*
         * Method to enter postcode
         */
        public void EnterPostCode(String postcodeEntered)
        {
            postcode.SendKeys(postcodeEntered);
        }


        /*
         * Method to enter phone number
         */
        public void EnterPhoneNumber(String phonenumber)
        {
            phoneNumber.SendKeys(phonenumber);
        }


        /*
         * Method to confirm order and click button
         */
        public void EnterOrder()
        {
            //Will check for stale error and confirm button is still present
            IWebElement place = driver.FindElement(placeOrderLocator);
            try
            {
                place.Click();
            }
            catch (Exception e)
            {
                //Handle the chrome error
                if(e is StaleElementReferenceException)
                {
                    //If fails find button again and then click on it
                    place = driver.FindElement(placeOrderLocator);
                    placeOrder.Click();
                 //Handle the firefox error
                } else if (e is ElementClickInterceptedException)
                {
                    Thread.Sleep(800);
                    placeOrder.Click();
                }
            }
        }


        /*
         * Method to return the order number
         */
        public int ReturnOrderNumber()
        {
            WaitForElementToDisplay(orderNumberLocator, 10, driver);
            return Convert.ToInt32(orderNumber.Text);
        }


        /*
         * Method to take user to their account
         */
        public void GoToMyAccount()
        {
            myAccount.Click();
        }

        /*
         * Method to click check payment button
         */
        public void ClickCheckPayments()
        {
            //Will check for stale error and confirm button is still present
            IWebElement check = driver.FindElement(checkPaymentsLocator);
            try
            {
                check.Click();
            }
            catch (Exception e)
            {
                //Handle the chrome error
                if(e is StaleElementReferenceException)
                {
                    //If fails find button again and then click on it
                    check = driver.FindElement(checkPaymentsLocator);
                    checkPayments.Click();
                //Handle the firefox error
                } else if (e is ElementClickInterceptedException)
                {
                    Thread.Sleep(800);
                    checkPayments.Click();
                }
            }
        }

    }
}
