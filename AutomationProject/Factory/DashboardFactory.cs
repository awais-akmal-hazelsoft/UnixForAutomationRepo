using Automation.Helper;
using Automation.WebApp;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProject.Factory
{
    public static class DashboardFactory
    {
        public static DashboardPage Build()
        {
            By dashboardHeading = By.CssSelector("h3.page-title");

            return new DashboardPage(WebDriver.Driver, dashboardHeading);
        }
    }
}
