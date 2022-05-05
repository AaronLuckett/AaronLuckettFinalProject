using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AaronLuckettFinalProject.PomPages
{
    public class OrdersNav
    {
        IWebDriver driver;

        /*
         * Constructor for class
         */
        public OrdersNav(IWebDriver driver)
        {
            this.driver = driver;
        }


        //Elements
        IWebElement orderNumber => driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a"));
        IWebElement logout => driver.FindElement(By.LinkText("Logout"));


        //Methods
        /*
         * Will click on the logout button
         */
        public void Logout()
        {
            logout.Click();
        }


        /*
         * Method to return the order number
         */
        public int GetOrderNumber()
        {
            //Trims to get only an integer
            return Convert.ToInt32(orderNumber.Text.TrimStart('#'));
        }
    }
}
