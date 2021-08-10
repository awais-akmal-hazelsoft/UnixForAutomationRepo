using System;
using NUnit.Framework;
using UnixFor.Pages.HardwareConfiguration;
using OpenQA.Selenium;
using UnixFor.Pages.Base;

namespace UnixFor.Tests
{
    [TestFixture]
    public class PeripheralTypesTests
    {
        PeripheralTypesPage peripheralTypesObj = new PeripheralTypesPage();
        [Test, Order(1)]
        public void TestPeripheralTypesOptionSelect()
        {
            peripheralTypesObj.ClickOnMenuOption(peripheralTypesObj.hardwareConfigurationMenu, peripheralTypesObj.peripheralTypes);
        }
        [Test, Order(2)]
        public void TestToAddInPeripheralTypes()
        {
            peripheralTypesObj.AddInPeripheralTypesPage("Test Name3", "Test Code3");
        }
        [Test, Order(3)]
        public void TestUpdateInPeripheralTypes()
        {
            peripheralTypesObj.UpdateInPeripheralTypes("TestName123", "TestName1", "TestCode1");
        }
        [Test, Order(4)]
        public void TestDeleteInPeripheralTypes()
        {
            string[] n = { "TestName123", "TestName2", "New Name", "Test Name2", "Test Code2" };
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
            peripheralTypesObj.IsElementDisable(peripheralTypesObj.modal);
        }
        [Test, Order(9)]
        public void TestColumnVisibility()
        {
            peripheralTypesObj.CheckColumnVisibility();
        }
        [Test, Order(10)]
        public void TestSorting()
        {
            peripheralTypesObj.CheckSorting();
        }
        [Test, Order(11)]
        public void TestOrdering()
        {
            peripheralTypesObj.CheckOrdering();
        }
    }
}
