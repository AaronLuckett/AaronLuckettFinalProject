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
        By DiscountAMountLocator = By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount");

        By FinalAmountLocator = By.CssSelector("strong > .amount.woocommerce-Price-amount");

        By RemoveFromCartLocator = By.CssSelector(".remove");
        IWebElement discount => driver.FindElement(DiscountAMountLocator);
        IWebElement final => driver.FindElement(FinalAmountLocator);
        IWebElement RemoveFromCart => driver.FindElement(RemoveFromCartLocator);
        IWebElement couponEntry => driver.FindElement(By.Id("coupon_code"));
        IWebElement totalAmount => driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount.woocommerce-Price-amount"));
        IWebElement delivry => driver.FindElement(By.CssSelector(".shipping > td > .amount.woocommerce-Price-amount"));
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
            WaitForElementToDisplay(DiscountAMountLocator, 10, driver);
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
            WaitForElementToDisplay(FinalAmountLocator, 10, driver);
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

        public void RemoveAllFromCart()
        {
            List<IWebElement> elementList = new List<IWebElement>();
            elementList.AddRange(driver.FindElements(RemoveFromCartLocator));
            if (elementList.Count > 0)
            {
                RemoveFromCart.Click();
            }
        }
    }
}
