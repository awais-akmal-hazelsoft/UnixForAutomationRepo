using Automation.Helper;
using AutomationProject.Factory;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AutomationProject.Tests
{
    [TestFixture]
    public class FilteringTests
    {
        IWebDriver _driver;

        Automation.WebApp.Filtering _filtering;
        //Automation.WebApp.DashboardPage _dashboard;

        [OneTimeSetUp]
        public void TestInit()
        {
            _driver = Singleton.Driver;
            _filtering = FilteringFactory.Build();
        }

        [Test]
        public void TestFiltering()
        {
            _filtering.ClickOnFilterButton();

        }
    }
}
