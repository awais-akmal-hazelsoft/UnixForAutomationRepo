using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using UnixFor.Pages.Base;
using OpenQA.Selenium.Interactions;
using UnixFor.Pages.Login;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace UnixFor.Pages.HardwareConfiguration
{
    class PeripheralTypesPage : BasePage
    {
        LoginPage loginObj = new LoginPage();
        //private By checkbox = By.XPath("//Table/tbody/tr[1]/td[1]/input");
        //private By editBtn = By.XPath("//Table/tbody/tr[5]/td[2]/div/button");
        private By textFieldName = By.CssSelector("input[name='name']");
        private By textFieldCode = By.CssSelector("input[name='code']");
        private By updateBtn = By.CssSelector("div.form__form-group>button");
        private By actionsDropdown = By.XPath("//div[@class='dropdown'][2]/button");
        private By deleteBtnInDropdown = By.XPath("//button[text()='Delete']");
        private By changeViewInDropdown = By.XPath("//button[text()='Change View']");
        private By okBtnToConfirmDelete = By.CssSelector("div>button.modal_ok");
 
        public By hardwareConfigurationMenu
        {
            get
            {
                return By.XPath("/html/body/div/div[2]/div/main/div/div[1]/div[2]/section/div[1]/div/div[1]/div/ul/div[1]/button");
            }
        }
        public By peripheralTypes
        {
            get
            {
                return By.CssSelector("a[href='/peripheralsTypes']");
            }
        }
        private By addBtn = By.XPath("//button[./span[text()='Add New']]");
        public By modal
        {
            get
            {
                return By.CssSelector("div.modal-content");
            }
        }
        
        private By checkboxInChangeViewModal = null;
        private By columnNameInChangeViewModal = null;

        private By adminBtn = By.XPath("//a[text()='Admin']");
        private By logoutBtn = By.XPath("//button[span[text()='Logout']]");
        public void CheckOrdering()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(IsElementVisible(tableHead), "Column name is not displayed");
            IWebElement from = GetElement(By.XPath("//table/thead/tr/th[3]"));
            string textBefore = GetElementText(By.XPath("//thead/tr/th[3]/div/span/span"));
            IWebElement to = GetElement(By.XPath("//table/thead/tr/th[4]"));
            Actions builder = new Actions(driver);
            builder.ClickAndHold(from).Perform();
            builder.Release(to).Perform();
            Assert.IsTrue(IsElementVisible(adminBtn), "Admin button is not displayed");
            GetElement(adminBtn).Click();
            GetElement(logoutBtn).Click();
            loginObj.InsertLoginDetails("Admin", "Admin!23");
            Assert.IsTrue(IsElementVisible(loginObj.dashboardHeading), "Dashboard Heading is not displayed");
            Assert.AreEqual(GetElementText(loginObj.dashboardHeading), "Dashboard", "Dashboard Text is not displayed as heading");
            Console.WriteLine("Login Successful");
            ClickOnMenuOption(hardwareConfigurationMenu, peripheralTypes);
            Assert.IsTrue(IsElementVisible(By.XPath("//table/thead/tr/th[3]")), "Column name is not displayed");
            string textAfter = GetElementText(By.XPath("//thead/tr/th[3]/div/span/span"));
            Assert.IsFalse(textBefore == textAfter, "Drag & drop column is not successful");
        }
        
        public void CheckSorting()
        {

            CheckDescendingOrder(3);
            driver.Navigate().Refresh();
            CheckAscendingOrder(3);


        }
        public ReadOnlyCollection<IWebElement> GetRows()
        {
            List<IWebElement> rows = new List<IWebElement>();
            Assert.IsTrue(IsElementVisible(By.XPath("//table")), "Table Element is Not displayed");
            IWebElement singleRow = null;
            for (int i = 0; i < 10; i++)
            {
                Assert.IsTrue(IsElementVisible(By.XPath("//div/table/tbody/tr[" + (i + 1) + "]")), "Element tr[" + i + "] is Not Displayed on screen");
                singleRow = GetElement(By.XPath("//div/table/tbody/tr[" + (i + 1) + "]"));
                rows.Add(singleRow);
            }
            ReadOnlyCollection<IWebElement> readOnlyRowsCollectionList = new ReadOnlyCollection<IWebElement>(rows);
            return readOnlyRowsCollectionList;
        }
        public ReadOnlyCollection<IWebElement> GetColumn(int colNumber)
        {
            List<IWebElement> cols = new List<IWebElement>();
            Assert.IsTrue(IsElementVisible(By.XPath("//table")), "Table Element is Not displayed");
            IWebElement singleCol = null;
            for (int i = 0; i < 10; i++)
            {
                Assert.IsTrue(IsElementVisible(By.XPath("//div/table/tbody/tr[" + (i + 1) + "]/td[" + colNumber + "]")), "Element tr[" + i + "] is Not Displayed on screen");
                singleCol = driver.FindElement(By.XPath("//div/table/tbody/tr[" + (i + 1) + "]/td[" + colNumber + "]"));
                cols.Add(singleCol);
            }
            ReadOnlyCollection<IWebElement> readOnlyRowsCollectionList = new ReadOnlyCollection<IWebElement>(cols);
            return readOnlyRowsCollectionList;
        }
        public IWebElement GetTableHead()
        {
            Assert.IsTrue(IsElementVisible(tableHead), "Table Head/Columns not displayed");
            return GetElement(tableHead);
        }
        public void SelectColumnInChangeViewModal(string[] arrayColumnName)
        {
            for (int i = 0; i < 2; i++)
            {
                checkboxInChangeViewModal = By.XPath("//div[@class='dragHandle-setting'][" + (i + 1) + "]/input");
                if (GetElement(checkboxInChangeViewModal).Selected)
                {
                    GetElement(checkboxInChangeViewModal).Click();
                }
            }
            for (int i = 0; i < 2; i++)
            {
                checkboxInChangeViewModal = By.XPath("//div[@class='dragHandle-setting'][" + (i + 1) + "]/input");
                columnNameInChangeViewModal = By.XPath("//div[@class='dragHandle-setting'][" + (i + 1) + " ]/span");
                for (int j = 0; j < arrayColumnName.Length; j++)
                {
                    if (arrayColumnName[j] == GetElementText(columnNameInChangeViewModal))
                    {
                        GetElement(checkboxInChangeViewModal).Click();
                    }
                }
            }
        }
        public void CheckColumnVisibility()
        {
            GetElement(actionsDropdown).Click();
            GetElement(changeViewInDropdown).Click();
            Assert.IsTrue(IsElementVisible(modal), "Change View Modal is not displayed");
            string[] arrayColNames = { "", "" };
            int totalColumns = 4;
            SelectColumnInChangeViewModal(arrayColNames);
            IWebElement submitBtn = GetElement(By.CssSelector("button.Full_Width"));
            submitBtn.Click();
            int countVisible = 0;
            for (int i = 2; i < arrayColNames.Length + 2; i++)
            {
                By col = By.XPath("//table/thead/tr/th[" + (i + 1) + "]");
                countVisible++;
            }
            Assert.IsTrue(totalColumns == countVisible + (totalColumns - arrayColNames.Length), "All Column(s) are not displayed");
        }
        public void CheckPaginationByChangingPage()
        {
            //paginationInfoNumbers[0] is starting Number of row in current page
            //paginationInfoNumbers[1] is Ending row in current page
            //paginationInfoNumbers[2] is total Number of rows in table
            int[] paginationInfoNumbers = { 0, 0, 0 };
            int visibleRows = 0;
            int totalRows = 0;
            do
            {
                FilterNumbers(ref paginationInfoNumbers);
                visibleRows = paginationInfoNumbers[1];
                totalRows = paginationInfoNumbers[2];
                if (visibleRows < totalRows)
                {
                    Assert.IsTrue(IsElementVisible(paginationNextBtn), "Pagination button is not visible");
                    driver.FindElement(paginationNextBtn).Click();
                }

            } while (visibleRows < totalRows);
        }
        public void CheckPaginationByChangingPageSize()
        {
            Assert.IsTrue(IsElementVisible(paginationSizingDropdown), "Dropdown for page sizing is not displayed");
            GetElement(paginationSizingDropdown).Click();
            Assert.IsTrue(IsElementVisible(paginationSizingDropdownItem), "Show 50 option is not displayed");
            GetElement(paginationSizingDropdownItem).Click();
            CheckPaginationByChangingPage();
        }
        public void UpdateInPeripheralTypes(string key, string updatedName, string updatedCode)
        {
            ReadOnlyCollection<IWebElement> tableRowsElements = GetRows();
            int tableRowsCount = tableRowsElements.Count;
            IWebElement inputCheckBox = null;
            IWebElement btnEdit = null;
            IWebElement name = null;
            IWebElement code = null;
            string testBtnEdit = "";
            for (int i = 1; i <= tableRowsCount; i++)
            {
                testBtnEdit = "//div/table/tbody/tr[" + i + "]/td[2]/div/button";
                inputCheckBox = GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[1]/input"));
                btnEdit = GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[2]/div/button"));
                name = GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[3]/span/span"));
                code = GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[4]/span/span"));
                if (name.Text == key)
                {
                    break;
                }
            }
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].innerText=''", name);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].innerText=''", code);
            By btn = By.XPath(testBtnEdit);
            Assert.IsTrue(IsElementVisible(btn), "Edit button is not displayed");
            btnEdit.Click();
            Assert.IsTrue(IsElementVisible(modal), "Update Modal is not displayed");
            GetElement(textFieldName).Clear();
            GetElement(textFieldCode).Clear();
            GetElement(textFieldName).SendKeys(updatedName);
            GetElement(textFieldCode).SendKeys(updatedCode);
            GetElement(updateBtn).Click();
            Assert.IsTrue(IsElementVisible(toastMessage), "Updated message is not displayed");
        }
        
        public void DeleteInPeripheralTypes(string[] arrayName)
        {
            selectCheckboxesToDelete(arrayName);
            Assert.IsTrue(IsElementVisible(actionsDropdown), "Dropdown is not displayed on Peripheral Types screen");
            GetElement(actionsDropdown).Click();
            Assert.IsTrue(IsElementVisible(deleteBtnInDropdown), "Delete in dropdown is not displayed on Peripheral types screen");
            GetElement(deleteBtnInDropdown).Click();
            Assert.IsTrue(IsElementVisible(okBtnToConfirmDelete), "Ok button to confirm delete operation is not displayed on peripheral Types Screen");
            GetElement(okBtnToConfirmDelete).Click();
        }
        public void selectCheckboxesToDelete(string[] arr)
        {
            ReadOnlyCollection<IWebElement> tableRowsElements = GetRows();
            int tableRowsCount = tableRowsElements.Count;
            IWebElement inputCheckBox = null;
            IWebElement btnEdit = null;
            IWebElement name = null;
            IWebElement code = null;
            for (int i = 1; i <= tableRowsCount; i++)
            {
                //testBtnEdit = "//div/table/tbody/tr[" + i + "]/td[2]/div/button";
                inputCheckBox = driver.FindElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[1]/input"));
                btnEdit = driver.FindElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[2]/div/button"));
                name = driver.FindElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[3]/span/span"));
                code = driver.FindElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[4]/span/span"));
                for (int j = 0; j < arr.Length; j++)
                {
                    if (name.Text == arr[j])
                    {
                        inputCheckBox.Click();
                    }
                }
            }
        }
        public void AddInPeripheralTypesPage(string n, string c)
        {
            Assert.IsTrue(IsElementVisible(addBtn), "Add Button is not displyed on peripheral types page");
            GetElement(addBtn).Click();
            Assert.IsTrue(IsElementVisible(modal), "Add modal is not dislayed");
            GetElement(textFieldName).SendKeys(n);
            GetElement(textFieldCode).SendKeys(c);
            Assert.IsTrue(IsElementVisible(updateBtn), "Update button in Add Modal");
            GetElement(updateBtn).Click();
            Assert.IsTrue(IsElementVisible(toastMessage), "Add message is not displayed");
        }
        public void IsElementDisable(By element)
        {
            Actions act = new Actions(driver);
            Assert.IsTrue(IsElementVisible(By.XPath("//table/tbody/tr")), "Table row not found");
            IWebElement row = GetElement(By.XPath("//table/tbody/tr"));
            act.DoubleClick(row).Perform();
            Assert.IsTrue(IsElementVisible(element), "Modal is not display");
            Assert.IsTrue(!GetElement(updateBtn).Enabled, "Update button is Enabled");
        }
    }
}
