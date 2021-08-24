using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Automation.WebApp
{
    public class DashboardPage : AutomationBase
    {
        By _dashboardHeading;

        public DashboardPage(IWebDriver driver, By dashboardHeading) 
            : base(driver)
        {
            _dashboardHeading = dashboardHeading;
        }

        public bool IsDashboardVisible()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(8));

            try
            {
                IWebElement searchElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_dashboardHeading));
                return searchElement.Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
