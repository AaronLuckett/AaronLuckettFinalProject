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
using static AaronLuckettFinalProject.Utilities.Helper;

namespace AaronLuckettFinalProject.StepDefinitions
{
    [Binding]
    public class PurcahsingProductsStepDefinitions: Hook
    {
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
        public void WhenIEnterTheCouponCode(string edgewords0)
        {
            ShopNav shopNav = new ShopNav(driver);
            shopNav.GoToBasketAfterPurchase();

            //Enters coupon code
            CartNav cartNav = new CartNav(driver);
            cartNav.enterCoupon(edgewords0);
        }


        [Then(@"a discount of '(.*)'% should be applied")]
        public void CorrectDiscount(string couponDiscount)
        {
            //First checks whether the correct discount from the coupon has been calculated
            CartNav cartNav = new CartNav(driver);
            //Get the corrosponding values
            Decimal Discount = cartNav.GetDiscountAmount();
            Decimal Before = cartNav.GetTotalBeforeDiscountAmount();
            Decimal ExpectedDiscount = (Math.Round(Before * (Convert.ToDecimal(couponDiscount) / 100),2));
            Boolean res = (ExpectedDiscount == Discount);

            cartNav.ScrollIntoView();

            //Take Screenshot
            TakesScreenshot(driver as ITakesScreenshot, "CouponCode");

            Assert.That(res, Is.True, "Expected a discount of £" + ExpectedDiscount + " but actual discount was £" + Discount);
            

            //Go to my account and logout
            cartNav.ProceedToMyAccount();
            MyAccountNav myAccountNav = new MyAccountNav(driver);
            myAccountNav.Logout();
        }


        [Then(@"The final cost should equal the item price minus the discount value")]
        public void ThenTheFinalCostShouldEqualTheItemPriceMinusTheDiscountValue()
        {
            CartNav cartNav = new CartNav(driver);

            //Then will check if that discount has been correctly
            //Gets the related values
            Decimal Delivery = cartNav.GetDeliverytAmount();
            Decimal Discount = cartNav.GetDiscountAmount();
            Decimal Before = cartNav.GetTotalBeforeDiscountAmount();
            Decimal Final = cartNav.GetFinalAmount();
            Decimal CalculatedFinal = (Delivery + (Before - Discount));

            Boolean res = (Final == CalculatedFinal);
            Assert.That(res, Is.True, "Expected a final value of £" + CalculatedFinal + " but got a value of £" + Final);
            Console.WriteLine("Finished Test");

            //Go to my account and logout
            cartNav.ProceedToMyAccount();
            MyAccountNav myAccountNav = new MyAccountNav(driver);
            myAccountNav.Logout();
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
            checkout.EnterUserDetails(Environment.GetEnvironmentVariable("First_Name"), Environment.GetEnvironmentVariable("Second_Name"),
                Environment.GetEnvironmentVariable("Address"), Environment.GetEnvironmentVariable("City"),
                Environment.GetEnvironmentVariable("Postcode"), Environment.GetEnvironmentVariable("Phone_Number"));

            //Click check payments and enter order
            checkout.ClickCheckPayments();
            checkout.EnterOrder();
        }


        [Then(@"The order number recevied after purachse should match the order number in order history")]
        public void ThenTheOrderNumberReceviedAfterPurachseShouldMatchTheOrderNumberInOrderHistory()
        {
            CheckoutNav checkout = new CheckoutNav(driver);
            int OrderNumber = checkout.ReturnOrderNumber();
            //Take Screenshot
            TakesScreenshot(driver as ITakesScreenshot, "OrderNumber1");
            checkout.GoToMyAccount();

            //Goes back to my account
            MyAccountNav myAccount = new MyAccountNav(driver);
            myAccount.GoToOrders();

            //Goes to the orders page and gets the order number
            OrdersNav order = new OrdersNav(driver);
            int OrderNumber2 = order.GetOrderNumber();
            //Take Screenshot
            TakesScreenshot(driver as ITakesScreenshot, "OrderNumber2");

            //Compares the two order numbers
            Assert.AreEqual(OrderNumber, OrderNumber2, "Expected " + OrderNumber + " to match " + " but order order number was " + OrderNumber2);

            order.Logout();
        }
    }
}
