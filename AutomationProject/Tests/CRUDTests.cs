using AutomationProject.Factory;
using Automation.Helper;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace AutomationProject.Tests
{
    [TestFixture]
    public class CRUDTests
    {
        IWebDriver _driver;

        Automation.WebApp.CRUD _CRUD;
        //Automation.WebApp.DashboardPage _dashboard;

        [OneTimeSetUp]
        public void TestInit()
        {
            _driver = Singleton.Driver;
            _CRUD = CRUDFactory.Build();
            //_dashboard = DashboardFactory.Build();
        }

        [Test, Order(1)]
        public void TestSelectOption()
        {
            _CRUD.SelectMenuOption();

        }

        [Test, Order(2)]
        public void TestAdd()
        {
            _CRUD.ClickOnAddButton();

            Assert.IsTrue(_CRUD.IsModalVisible(), "Add Modal is not displayed");
            
            _CRUD.SetValueInNameTextbox("Value From Json");
            _CRUD.SetValueInCodeTextbox("Value From Json");

            _CRUD.ClickOnModalActionButton();

            Assert.IsTrue(_CRUD.IsToastMessageVisible(), "Toast message is not displayed");
        }

        [Test, Order(3)]
        public void TestUpdate()
        {
            _CRUD.MatchingRecordForEdit("Value from json");
            
            Assert.IsTrue(_CRUD.IsModalVisible(), "Update modal is not displayed");
            
            _CRUD.ClearNameTextbox();
            _CRUD.ClearCodeTextbox();

            _CRUD.SetValueInNameTextbox("Value From Json");
            _CRUD.SetValueInCodeTextbox("Value From Json");

            _CRUD.ClickOnModalActionButton();
        }

        [Test, Order(4)]
        public void TestDelete()
        {
            _CRUD.SelectRecordForDelete("Value from json");
          
            _CRUD.ClickOnActionDropdown();
            _CRUD.ClickOnDeleteInActionDropdown();
            _CRUD.ClickOnDeleteOkButton();

            Assert.IsTrue(_CRUD.IsToastMessageVisible(), "Delete toast message is not displayed");           
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _driver.Close();
            _driver.Quit();
        }

    }
}
