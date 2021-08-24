using Automation.Helper;
using AutomationProject.Factory;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AutomationProject.Tests
{
    [TestFixture]
    public class ColumnOrderingTests
    {
        IWebDriver _driver;

        Automation.WebApp.ColumnOrdering _ColumnOrderingObj;
        //Automation.WebApp.DashboardPage _dashboard;

        [OneTimeSetUp]
        public void TestInit()
        {
            _driver = WebDriver.Driver;
            _ColumnOrderingObj = ColumnOrderingFactory.Build();
            //_dashboard = DashboardFactory.Build();
        }

        [Test]
        public void TestColumnOrdering()
        {
            _ColumnOrderingObj.DragColumn();
            _ColumnOrderingObj.LogOut();
            _ColumnOrderingObj.LogIn();
        }
    }
}
