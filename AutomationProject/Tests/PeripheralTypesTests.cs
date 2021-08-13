using System;
using NUnit.Framework;
using UnixFor.Pages.HardwareConfiguration;
using OpenQA.Selenium;
using UnixFor.Pages.Base;
using UnixFor.Helper;

namespace UnixFor.Tests
{
    [TestFixture]
    public class PeripheralTypesTests
    {
        PeripheralTypesPage peripheralTypesObj = new PeripheralTypesPage();

        [Test, Order(1)]
        public void TestPeripheralTypesOptionSelect()
        {
            CommonFunctions.ClickOnMenuOption(PeripheralTypesPage.hardwareConfigurationMenu, PeripheralTypesPage.peripheralTypes);
        }

        [Test, Order(2)]
        public void TestToAddInPeripheralTypes()
        {
            peripheralTypesObj.AddInPeripheralTypesPage("Test Name4", "Test Code4");
        }

        [Test, Order(3)]
        public void TestUpdateInPeripheralTypes()
        {
            peripheralTypesObj.UpdateInPeripheralTypes("Test Name4", "Test Name1", "Test Code1");
        }

        [Test, Order(4)]
        public void TestDeleteInPeripheralTypes()
        {
            string[] n = { "Test Name4"};
            peripheralTypesObj.DeleteInPeripheralTypes(n);
        }

        [Test, Order(5)]
        public void TestColumnVisibility()
        {
            string[] columnNames = { "Code", "Name" };
            peripheralTypesObj.CheckColumnVisibility(columnNames);
        }

        [Test, Order(6)]
        public void TestSortColumn()
        {
            peripheralTypesObj.CheckSorting();
        }

        [Test, Order(7)]
        public void TestChangeColumnOrder()
        {

            CommonFunctions.CheckOrdering(Pages.Login.LoginPage.Instance.InsertLoginDetails);
        }

        [Test, Order(8)]
        public void TestFilter()
        {
            CommonFunctions.WaitUntilInvisible(CommonFunctions.loadingSpinner);
            CommonFunctions.CheckFilter("name", Constants.FilterNotEqualValue, "test");
        }

        [Test, Order(9)]
        public void TestPaginationByChangingPage()
        {
            CommonFunctions.driver.Navigate().Refresh();
            CommonFunctions.WaitUntilInvisible(CommonFunctions.loadingSpinner);
            peripheralTypesObj.CheckPaginationByChangingPage();
        }

        [Test, Order(10)]
        public void TestPaginationByChangingPageSize()
        {
            CommonFunctions.driver.Navigate().Refresh();
            CommonFunctions.WaitUntilInvisible(CommonFunctions.loadingSpinner);
            peripheralTypesObj.CheckPaginationByChangingPageSize();
        }

        [Test, Order(11)]
        public void TestIsModalReadOnly()
        {
            CommonFunctions.CheckReadOnlyView(CommonFunctions.modal);
        }

        [OneTimeTearDown]
        public void TestEnd()
        {
            CommonFunctions.driver.Quit();
        }

    }
}
