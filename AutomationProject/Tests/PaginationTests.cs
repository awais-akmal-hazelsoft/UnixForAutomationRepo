using Automation.Helper;
using AutomationProject.Factory;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace AutomationProject.Tests
{
    [TestFixture]
    public class PaginationTests
    {
        IWebDriver _driver;

        Automation.WebApp.Pagination _pagination;
        //Automation.WebApp.DashboardPage _dashboard;

        [OneTimeSetUp]
        public void TestInit()
        {
            _driver = WebDriver.Driver;
            _pagination = PaginationFactory.Build();
            //_dashboard = DashboardFactory.Build();
        }

        [Test]
        public void TestPaginationByChangingPage()
        {
            do
            {
                
                _pagination.GetElement(_pagination.GetPaginationNextButton()).Click();
            } while (_pagination.GetElement(_pagination.GetPaginationNextButton()).Enabled);
        }

        public void TestPaginationByChangingPageSize()
        {
            _pagination.GetElement(_pagination.GetPaginationSizingDropdown()).Click();
            _pagination.GetElement(_pagination.GetPaginationSizingDropdownItem()).Click();
            
            do
            {
                _pagination.GetElement(_pagination.GetPaginationNextButton()).Click();
            } while (_pagination.GetElement(_pagination.GetPaginationNextButton()).Enabled);

        }
    }
}
