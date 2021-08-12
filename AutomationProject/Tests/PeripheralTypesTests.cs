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
            peripheralTypesObj.ClickOnMenuOption(PeripheralTypesPage.hardwareConfigurationMenu, PeripheralTypesPage.peripheralTypes);
        }

        [Test, Order(2)]
        public void TestToAddInPeripheralTypes()
        {
            peripheralTypesObj.AddInPeripheralTypesPage("Test Name4", "Test Code4");
        }

        [Test, Order(3)]
        public void TestUpdateInPeripheralTypes()
        {
            peripheralTypesObj.UpdateInPeripheralTypes("Test Name1", "Test Name4", "Test Code4");
        }

        [Test, Order(4)]
        public void TestDeleteInPeripheralTypes()
        {
            string[] n = { "New Name", "TestName2" };
            peripheralTypesObj.DeleteInPeripheralTypes(n);
        }

        [Test, Order(6)]
        public void TestPaginationByChangingPage()
        {
            peripheralTypesObj.CheckPaginationByChangingPage();
        }

        [Test, Order(7)]
        public void TestPaginationByChangingPageSize()
        {
            peripheralTypesObj.CheckPaginationByChangingPageSize();
        }

        [Test, Order(8)]
        public void TestIsModalReadOnly()
        {
            peripheralTypesObj.CheckReadOnlyView(peripheralTypesObj.modal);
        }

        [Test, Order(9)]
        public void TestColumnVisibility()
        {
            string[] columnNames = { "Code", "Name" };
            peripheralTypesObj.CheckColumnVisibility(columnNames);
        }

        [Test, Order(10)]
        public void TestSortColumn()
        {
            peripheralTypesObj.CheckSorting();
        }

        [Test, Order(11)]
        public void TestChangeColumnOrder()
        {
            peripheralTypesObj.CheckOrdering();
        }

        [Test, Order(12)]
        public void TestFilter()
        {
            peripheralTypesObj.CheckFilter("name", Constants.FilterContainValue, "test");
        }

    }
}
