using AutomationProject.Factory;
using AutomationProject.Helper;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace AutomationProject.Tests
{
    [TestFixture]
    public class GenericTests
    {
        IWebDriver _driver;

        Automation.WebApp.GenericPage _genericPage;
        //Automation.WebApp.DashboardPage _dashboard;

        [OneTimeSetUp]
        public void TestInit()
        {
            _driver = Singleton.Driver;
            _genericPage = GenericPageFactory.Build();
            //_dashboard = DashboardFactory.Build();
        }

        [Test]
        public void TestReadOnly()
        {
            _genericPage.DoubleClickOnRowFirstRow();
            
            Assert.IsTrue(_genericPage.IsModalDisplay(), "Modal is not displayed");
            Assert.IsTrue(_genericPage.IsModalDisable(), "Modal fields are not displayed");
        }
    }
}
