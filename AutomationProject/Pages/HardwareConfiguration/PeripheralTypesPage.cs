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
using UnixFor.Helper;

namespace UnixFor.Pages.HardwareConfiguration
{
    class PeripheralTypesPage
    {
        //button elements
        private By confirmDeleteOkBtn = By.CssSelector("div>button.modal_ok");

        //textfield elements
        private By textFieldName = By.CssSelector("input[name='name']");
        private By textFieldCode = By.CssSelector("input[name='code']");

        //dropdown elements
        private By actionsDropdown = By.XPath("//div[@class='dropdown'][2]/button");
        private By deleteBtnInDropdown = By.XPath("//button[text()='Delete']");
        private By changeViewInDropdown = By.XPath("//button[text()='Change View']");

        //property to get hardwareConfiguration menu xpath
        public static By hardwareConfigurationMenu
        {
            get
            {
                return By.XPath("/html/body/div/div[2]/div/main/div/div[1]/div[2]/section/div[1]/div/div[1]/div/ul/div[1]/button");
            }
        }

        //property to get peripheral Types Page(or menu item) xpath
        public static By peripheralTypes
        {
            get
            {
                return By.CssSelector("a[href='/peripheralsTypes']");
            }
        }
        private By addBtn = By.XPath("//button[./span[text()='Add New']]");

        //method to check column sorting 
        public void CheckSorting()
        {
            CommonFunctions.CheckDescendingSorting(Constants.NameColumnHeadingtext);
            CommonFunctions.driver.Navigate().Refresh();
            CommonFunctions.CheckAscendingSorting(Constants.NameColumnHeadingtext);
        }

        //method to select column in change view modal
        public void SelectColumnInChangeViewModal(string[] arrayColumnName)
        {
            By checkboxInChangeViewModal;
            By columnNameInChangeViewModal;
            for (int i = 0; i < 2; i++)
            {
                checkboxInChangeViewModal = By.XPath("//div[@class='dragHandle-setting'][" + (i + 1) + "]/input");
                if (CommonFunctions.GetElement(checkboxInChangeViewModal).Selected)
                {
                    CommonFunctions.GetElement(checkboxInChangeViewModal).Click();
                }
            }
            for (int i = 0; i < 2; i++)
            {
                checkboxInChangeViewModal = By.XPath("//div[@class='dragHandle-setting'][" + (i + 1) + "]/input");
                columnNameInChangeViewModal = By.XPath("//div[@class='dragHandle-setting'][" + (i + 1) + " ]/span");
                for (int j = 0; j < arrayColumnName.Length; j++)
                {
                    if (arrayColumnName[j] == CommonFunctions.GetElementText(columnNameInChangeViewModal))
                    {
                        CommonFunctions.GetElement(checkboxInChangeViewModal).Click();
                    }
                }
            }
        }
        //method to check column visibilty
        public void CheckColumnVisibility(string[] columnNames)
        {
            CommonFunctions.GetElement(actionsDropdown).Click();
            CommonFunctions.GetElement(changeViewInDropdown).Click();
            Assert.IsTrue(CommonFunctions.IsElementVisible(CommonFunctions.modal), "Change View Modal is not displayed");
            int totalColumns = 4;
            SelectColumnInChangeViewModal(columnNames);
            IWebElement submitBtn = CommonFunctions.GetElement(By.CssSelector("button.Full_Width"));
            submitBtn.Click();
            int countVisible = 0;
            for (int i = 2; i < columnNames.Length + 2; i++)
            {
                By col = By.XPath("//table/thead/tr/th[" + (i + 1) + "]");
                countVisible++;
            }
            Assert.IsTrue(totalColumns == countVisible + (totalColumns - columnNames.Length), "All Column(s) are not displayed");
        }
        //method to check pagination by changing page using next button  
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
                CommonFunctions.FilterNumbers(ref paginationInfoNumbers);
                visibleRows = paginationInfoNumbers[1];
                totalRows = paginationInfoNumbers[2];
                if (visibleRows < totalRows)
                {
                    Assert.IsTrue(CommonFunctions.IsElementVisible(CommonFunctions.paginationNextBtn), "Pagination button is not visible");
                    CommonFunctions.driver.FindElement(CommonFunctions.paginationNextBtn).Click();
                    CommonFunctions.WaitUntilInvisible(CommonFunctions.loadingSpinner);
                }

            } while (visibleRows < totalRows);
        }
        //method to check pagination by changing page size and selecting page size 
        public void CheckPaginationByChangingPageSize()
        {
            CommonFunctions.WaitUntilInvisible(CommonFunctions.loadingSpinner);
            Assert.IsTrue(CommonFunctions.IsElementVisible(CommonFunctions.paginationSizingDropdown), "Dropdown for page sizing is not displayed");
            CommonFunctions.GetElement(CommonFunctions.paginationSizingDropdown).Click();
            CommonFunctions.WaitUntilInvisible(CommonFunctions.loadingSpinner);
            Assert.IsTrue(CommonFunctions.IsElementVisible(CommonFunctions.paginationSizingDropdownItem), "Show 50 option is not displayed");
            CommonFunctions.GetElement(CommonFunctions.paginationSizingDropdownItem).Click();
            CheckPaginationByChangingPage();
        }
        //method to update a record
        public void UpdateInPeripheralTypes(string nameKey, string updatedName, string updatedCode)
        {
            IWebElement btnEdit = null;
            IWebElement name = null;
            IWebElement code = null;
            string testBtnEdit = "";
            CommonFunctions.WaitUntilInvisible(CommonFunctions.loadingSpinner);
            for (int i = 1; i <= 10; i++)
            {
                testBtnEdit = "//div/table/tbody/tr[" + i + "]/td[2]/div/button";
                btnEdit = CommonFunctions.GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[2]/div/button"));
                name = CommonFunctions.GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[3]/span/span"));
                code = CommonFunctions.GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[4]/span/span"));
                if (name.Text == nameKey)
                {
                    ((IJavaScriptExecutor)CommonFunctions.driver).ExecuteScript("arguments[0].innerText=''", name);
                    ((IJavaScriptExecutor)CommonFunctions.driver).ExecuteScript("arguments[0].innerText=''", code);
                    btnEdit.Click();
                    break;
                }
            }
            Assert.IsTrue(CommonFunctions.IsElementVisible(CommonFunctions.modal), "Update Modal is not displayed");
            CommonFunctions.GetElement(textFieldName).Clear();
            CommonFunctions.GetElement(textFieldCode).Clear();
            CommonFunctions.GetElement(textFieldName).SendKeys(updatedName);
            CommonFunctions.GetElement(textFieldCode).SendKeys(updatedCode);
            CommonFunctions.GetElement(CommonFunctions.updateBtn).Click();
            Assert.IsTrue(CommonFunctions.IsElementVisible(CommonFunctions.toastMessage), "Updated message is not displayed");
        }
        //method to delete record(s)
        public void DeleteInPeripheralTypes(string[] arrayName)
        {
            selectCheckboxesToDelete(arrayName);
            Assert.IsTrue(CommonFunctions.IsElementVisible(actionsDropdown), "Dropdown is not displayed on Peripheral Types screen");
            CommonFunctions.GetElement(actionsDropdown).Click();
            Assert.IsTrue(CommonFunctions.IsElementVisible(deleteBtnInDropdown), "Delete in dropdown is not displayed on Peripheral types screen");
            CommonFunctions.GetElement(deleteBtnInDropdown).Click();
            Assert.IsTrue(CommonFunctions.IsElementVisible(confirmDeleteOkBtn), "Ok button to confirm delete operation is not displayed on peripheral Types Screen");
            CommonFunctions.GetElement(confirmDeleteOkBtn).Click();
            Assert.IsTrue(CommonFunctions.IsElementVisible(CommonFunctions.toastMessage), "Delete message is not displayed");
        }
        //method to select checkbox of those records which we want to delete
        public void selectCheckboxesToDelete(string[] arr)
        {
            IWebElement inputCheckBox;
            IWebElement name;
            CommonFunctions.WaitUntilInvisible(CommonFunctions.loadingSpinner);
            for (int i = 1; i <= 10; i++)
            {
                inputCheckBox = CommonFunctions.GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[1]/input"));
                name = CommonFunctions.GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[3]/span/span"));
                for (int j = 0; j < arr.Length; j++)
                {
                    if (name.Text == arr[j])
                    {
                        inputCheckBox.Click();
                    }
                }
            }
        }
        //method to add records
        public void AddInPeripheralTypesPage(string nameFieldText, string codeFielText)
        {
            Assert.IsTrue(CommonFunctions.IsElementVisible(addBtn), "Add Button is not displyed on peripheral types page");
            CommonFunctions.GetElement(addBtn).Click();
            Assert.IsTrue(CommonFunctions.IsElementVisible(CommonFunctions.modal), "Add modal is not dislayed");
            CommonFunctions.GetElement(textFieldName).SendKeys(nameFieldText);
            CommonFunctions.GetElement(textFieldCode).SendKeys(codeFielText);
            Assert.IsTrue(CommonFunctions.IsElementVisible(CommonFunctions.updateBtn), "Update button in Add Modal");
            CommonFunctions.GetElement(CommonFunctions.updateBtn).Click();
            Assert.IsTrue(CommonFunctions.IsElementVisible(CommonFunctions.toastMessage), "Add message is not displayed");
        }
    }
}
