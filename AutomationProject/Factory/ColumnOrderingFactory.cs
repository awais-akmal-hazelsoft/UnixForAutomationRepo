using Automation.Helper;
using Automation.WebApp;
using OpenQA.Selenium;
using System;


namespace AutomationProject.Factory
{
    class ColumnOrderingFactory
    {
        public static ColumnOrdering Build()
        {
            var parameters = new ColumnOrderingParameters
            {
                FirstColumn = By.XPath("//table/thead/tr/th[3]"),
                SecondColumn = By.XPath("//table/thead/tr/th[4]"),
                AdminButton = By.XPath("//a[text()='Admin']"),
                LogOutButton = By.XPath("//button[span[text()='Logout']]"),

            };

            return new ColumnOrdering(Singleton.Driver, parameters);
        }
    }
}
