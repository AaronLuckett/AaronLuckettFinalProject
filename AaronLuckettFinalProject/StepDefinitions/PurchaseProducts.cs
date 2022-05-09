using System;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AaronLuckettFinalProject.PomPages;
using AaronLuckettFinalProject.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Data;
using static AaronLuckettFinalProject.Utilities.Helper;
using static AaronLuckettFinalProject.Utilities.Hook;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AaronLuckettFinalProject.StepDefinitions
{
    [Binding]
    public class PurchasingProductsStepDefinitions
    {

        IWebDriver driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly CustomerData _customerData;

        public PurchasingProductsStepDefinitions(ScenarioContext scenarioContext, CustomerData customerData)
        {
            _scenarioContext = scenarioContext;
            this.driver = (IWebDriver)_scenarioContext["webdriver"];
            this._customerData = customerData;
        }

        [Given(@"I have logged in")]
        public void GivenIHaveLoggedIn()
        {
            //Login and go to shop
            MyAccountNav myAccount = new MyAccountNav(driver);
            myAccount.Login(Environment.GetEnvironmentVariable("SECRET_USERNAME"), Environment.GetEnvironmentVariable("SECRET_PASSWORD"));
        }


        [Given(@"I am on the main shop page")]
        public void GivenIAmOnTheMainShopPage()
        {
            MyAccountNav myAccount = new MyAccountNav(driver);
            myAccount.GoShop();
        }


        [When(@"I add an item to my cart")]
        public void WhenIAddAnItemToMyCart()
        {
            //Adds item to cart and goes to basket
            ShopNav shopNav = new ShopNav(driver);
            shopNav.AddBeltToCart();
        }


        [When(@"I enter the coupon code '(.*)'")]
        public void WhenIEnterTheCouponCode(string couponName)
        {
            ShopNav shopNav = new ShopNav(driver);
            shopNav.GoToBasketAfterPurchase();

            //Enters coupon code
            CartNav cartNav = new CartNav(driver);
            cartNav.EnterCoupon(couponName);
        }


        [Then(@"a discount of '(.*)'% should be applied")]
        public void CorrectDiscount(string couponDiscount)
        {
            //First checks whether the correct discount from the coupon has been calculated
            CartNav cartNav = new CartNav(driver);
            //Get the corrosponding values
            Decimal discount = cartNav.GetDiscountAmount();
            Decimal before = cartNav.GetTotalBeforeDiscountAmount();
            Decimal expectedDiscount = (Math.Round(before * (Convert.ToDecimal(couponDiscount) / 100),2));
            Boolean res = (expectedDiscount == discount);

            cartNav.ScrollIntoView();

            //Take Screenshot
            TakesScreenshot(driver as ITakesScreenshot, "CouponCode");

            Assert.That(res, Is.True, "Expected a discount of £" + expectedDiscount + " but actual discount was £" + discount);
        }


        [Then(@"The final cost should equal the item price minus the discount value")]
        public void ThenTheFinalCostShouldEqualTheItemPriceMinusTheDiscountValue()
        {
            CartNav cartNav = new CartNav(driver);

            //Then will check if that discount has been correctly
            //Gets the related values
            Decimal delivery = cartNav.GetDeliverytAmount();
            Decimal discount = cartNav.GetDiscountAmount();
            Decimal before = cartNav.GetTotalBeforeDiscountAmount();
            Decimal final = cartNav.GetFinalAmount();
            Decimal calculatedFinal = (delivery + (before - discount));

            Boolean res = (final == calculatedFinal);
            Assert.That(res, Is.True, "Expected a final value of £" + calculatedFinal + " but got a value of £" + final);
            Console.WriteLine("Finished Test");
        }


        [When(@"I Complete the purchase by filling in my correct details")]
        public void GivenCompleteThePurchaseByFillingInMyCorrectDetails()
        {
            //Adds item to cart and goes to basket
            ShopNav shopNav = new ShopNav(driver);
            shopNav.GoToBasketAfterPurchase();

            CartNav cartNav = new CartNav(driver);
            cartNav.ProceedToCheckout();

            //Goes to the checkout page
            CheckoutNav checkout = new CheckoutNav(driver);

            //Enter user details
            checkout.EnterUserDetails(_customerData.firstName, _customerData.lastName,
                _customerData.address, _customerData.city, _customerData.postcode, _customerData.phoneNumber);

            //Click check payments and enter order
            checkout.ClickCheckPayments();
            checkout.EnterOrder();
        }


        [Then(@"The order number recevied after purachse should match the order number in order history")]
        public void ThenTheOrderNumberReceviedAfterPurachseShouldMatchTheOrderNumberInOrderHistory()
        {
            CheckoutNav checkout = new CheckoutNav(driver);
            int orderNumber = checkout.ReturnOrderNumber();
            //Take Screenshot
            TakesScreenshot(driver as ITakesScreenshot, "OrderNumberOne");
            checkout.GoToMyAccount();

            //Goes back to my account
            MyAccountNav myAccount = new MyAccountNav(driver);
            myAccount.GoToOrders();

            //Goes to the orders page and gets the order number
            OrdersNav order = new OrdersNav(driver);
            int orderNumber2 = order.GetOrderNumber();
            //Take Screenshot
            TakesScreenshot(driver as ITakesScreenshot, "OrderNumberTwo");

            //Compares the two order numbers
            Assert.AreEqual(orderNumber, orderNumber2, "Expected " + orderNumber + " to match " + "but other order number was " + orderNumber2);
        }
    }
}
