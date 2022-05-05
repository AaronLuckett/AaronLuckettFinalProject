using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using static AaronLuckettFinalProject.Utilities.Helper;

namespace AaronLuckettFinalProject.PomPages
{
    public class CartNav
    {
        private IWebDriver driver;

        /*
         * Constructor for class
         */
        public CartNav(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Elements
        IWebElement couponEntry => driver.FindElement(By.Id("coupon_code"));
        IWebElement discount => driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount"));
        IWebElement totalAmount => driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount.woocommerce-Price-amount"));
        IWebElement delivry => driver.FindElement(By.CssSelector(".shipping > td > .amount.woocommerce-Price-amount"));
        IWebElement final => driver.FindElement(By.CssSelector("strong > .amount.woocommerce-Price-amount"));
        IWebElement couponEnter => driver.FindElement(By.Name("apply_coupon"));
        IWebElement proceedToCheckout => driver.FindElement(By.LinkText("Proceed to checkout"));
        IWebElement proceedToMyAccount => driver.FindElement(By.LinkText("My account"));


        //Methods
        /*
         * Method to enter coupon code
         */
        public void enterCoupon(String couponCode)
        {
            couponEntry.SendKeys(couponCode);
            couponEnter.Click();
        }


        /*
         * Method to get discount amount
         */
        public Decimal GetDiscountAmount()
        {
            WaitForElementToDisplay(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount"), 10, driver);
            return Convert.ToDecimal(discount.Text.TrimStart('£'));
        }


        /*
         * Method to get price before discount
         */
        public Decimal GetTotalBeforeDiscountAmount()
        {
            return Convert.ToDecimal(totalAmount.Text.TrimStart('£'));
        }


        /*
         * Method to get price of delivery
         */
        public Decimal GetDeliverytAmount()
        {
            return Convert.ToDecimal(delivry.Text.TrimStart('£'));
        }


        /*
         * Method to get price after discount applied
         */
        public Decimal GetFinalAmount()
        {
            WaitForElementToDisplay(By.CssSelector("strong > .amount.woocommerce-Price-amount"), 10, driver);
            return Convert.ToDecimal(final.Text.TrimStart('£'));
        }


        /*
         * Method to take user to checkout
         */
        public void ProceedToCheckout()
        {
            proceedToCheckout.Click();
        }

        /*
         * Method to take user to my account
         */
        public void ProceedToMyAccount()
        {
            proceedToMyAccount.Click();
        }

        /*
         * Scrolls prices into view
         */
        public void ScrollIntoView()
        {
            var actions = new Actions(driver);
            actions.MoveToElement(proceedToCheckout);
            actions.Perform();
        }
    }
}
