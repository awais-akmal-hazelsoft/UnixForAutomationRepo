
//using System;
//using NUnit.Framework;
//using AutomationProject.Pages.Base;
//using System.Collections.Generic;
//using System.Text;
//using OpenQA.Selenium;

//namespace AutomationProject.Tests
//{
//    /// <summary>
//    /// Summary description for UnitTest1
//    /// </summary>

//    [TestFixture]
//    public class TestBase
//    {
//        private IWebDriver driver;
//        BasePage baseObj = new BasePage();
//        public TestBase()
//        {
//            this.driver = baseObj.GetDriver();
//        }

//        //Test to open browser window
//        [OneTimeSetUp]
//        public void TestInit()
//        {
//            driver.Url = "http://unixfor.hazelsoft.net/";
//        }
//        //******************Test 6 CRUD*****************************************
//        [Test, Order(4)]
//        public void TestPeripheralTypesCRUD()
//        {
//            baseObj.ClickOnMenuOption(PeripheralTypesPage.hardwareConfigurationMenu, PeripheralTypesPage.peripheralTypes);
//        }
//        //*******************Test Update in peripherals Types screen*******************************
//        [Test, Order(5)]
//        public void TestUpdateInPeripheralTypes()
//        {
//            PeripheralTypesPage.UpdateInPeripheralTypes("TestName1", "TestName123", "TestCode123");
//        }
//        [Test, Order(7)]
//        public void TestDeleteInPeripheralTypes()
//        {
//            string[] n = { "TestName123", "TestName2", "New Name", "TestName1", "Barcode/QR Scanner" };
//            //string[] c = { "Updated TestCode", "Test Code", "New Name", ""};
//            PeripheralTypesPage.DeleteInPeripheralTypes(n);
//        }
//        //[Test, Order(7)]
//        //public static void TestToActiveDeletePeripheralTypes()
//        //{
//        //    Thread.Sleep(5000);
//        //    PeripheralTypesPage.ActiveRow();
//        //}
//        [Test, Order(6)]
//        public void TestToAddInPeripheralTypes()
//        {
//            PeripheralTypesPage.AddInPeripheralTypesPage();
//        }
//        [Test, Order(7)]
//        public void TestPaginationByChangingPage()
//        {
//            PeripheralTypesPage.CheckPaginationByChangingPage();
//        }
//        [Test, Order(8)]
//        public static void TestPaginationByChangingPageSize()
//        {
//            PeripheralTypesPage.CheckPaginationByChangingPageSize();
//        }
//        //******************Test  to test to close browser*********************** 
//        [OneTimeTearDown]
//        public void EndTest()
//        {

//            //driver.Quit();
//            Console.WriteLine("***  Browser Window Close  ***");
//        }

//    }

//}
