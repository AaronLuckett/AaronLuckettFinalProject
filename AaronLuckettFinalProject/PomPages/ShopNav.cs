﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using static AaronLuckettFinalProject.Utilities.Helper;

namespace AaronLuckettFinalProject.PomPages
{
    internal class ShopNav
    {
        private IWebDriver driver;

        /*
         * Constructor for class
         */
        public ShopNav(IWebDriver driver)
        {
            this.driver = driver;
        }


        //Locators/Elements
        IWebElement beltButton => driver.FindElement(By.CssSelector(".has-post-thumbnail.instock.post-28.product.product-type-simple.product_cat-accessories.purchasable.sale.shipping-taxable.status-publish.type-product > .add_to_cart_button.ajax_add_to_cart.button.product_type_simple"));
        IWebElement GoToCart => driver.FindElement(By.LinkText("View cart"));


        //Services and Methods
        /*
         * Method which adds a belt to the cart 
         */
        public void AddBeltToCart()
        {
            //Moves to the element and clicks
            Actions action = new Actions(driver);
            action.MoveToElement(beltButton);
            action.Perform();
            beltButton.Click();
        }


        /*
         * Method to take user to basket
         */
        public void GoToBasketAfterPurchase()
        {
            WaitForElementToDisplay(By.LinkText("View cart"), 10, driver);
            GoToCart.Click();
        }
    }
}
