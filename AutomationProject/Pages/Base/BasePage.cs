using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnixFor.Pages.Login;
using UnixFor.Pages.HardwareConfiguration;
using UnixFor.Helper;
using System.Text.RegularExpressions;

namespace UnixFor.Pages.Base
{
    public abstract class BasePage
    {
               /******************************************************************************************
                                               Rejected Code
                ******************************************************************************************/
        protected static IWebDriver driver;
        //button elements
        protected By svgBtn = By.XPath("(//div[@title='Toggle SortBy'])[1]");
        protected By paginationNextBtn = By.XPath("(//li[@class='pagination__item page-item']/button[@class='pagination__link pagination__link--arrow page-link'])[2]");
        protected By updateBtn = By.CssSelector("div.form__form-group>button");
        protected By adminBtn = By.XPath("//a[text()='Admin']");
        protected By logoutBtn = By.XPath("//button[span[text()='Logout']]");
        protected By filterBtn = By.XPath("//button[./span[text()='Filter']]");
        protected By applyFilterBtn = By.XPath("//button[text()='Apply Filter']");
        //Dropdown elements
        protected By paginationSizingDropdown = By.XPath("//select[@id='exampleSelect']");
        protected By paginationSizingDropdownItem = By.XPath("//select[@id='exampleSelect' ]//option[@Value='50']");
        //others elements
        protected By ascendingOrderSvg = By.XPath("//th[@class='thead tr wordbreak-audit']//*[local-name()='svg']/*[local-name()='path' and @d='M19 17H22L18 21L14 17H17V3H19M2 17H12V19H2M6 5V7H2V5M2 11H9V13H2V11Z']");
        protected By decendingOrderSvg = By.XPath("//th[@class='thead tr wordbreak-audit']//*[local-name()='svg']/*[local-name()='path' and @d='M18 21L14 17H17V7H14L18 3L22 7H19V17H22M2 19V17H12V19M2 13V11H9V13M2 7V5H6V7H2Z']");
        protected By loadingSpinner = By.CssSelector("div.spinner");
        protected By paginationInfoList = By.CssSelector("nav.pagination>ul li.pagination-info");
        protected By tableHead = By.XPath("//table/thead/tr");
        protected By filterNameDropdown = By.Name("field0");
        protected By filterTypeDropdown = By.Name("type0");
        protected By filterValueDropDown = By.Name("value0");
        //property to get dashboard heading 
        public By dashboardHeading
        {
            get
            {
                return By.CssSelector("h3.page-title");
            }
        }
        //property to get modal for CRUD
        public By modal
        {
            get
            {
                return By.CssSelector("div.modal-content");
            }
        }
        //property to get errortoast message
        public By toastMessage
        {
            get
            {
                return By.CssSelector("div.Toastify__toast-body");
            }
        }

        // Method To check either element is visible on page or not  
        public bool IsElementVisible(By element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));

            try
            {
                IWebElement searchElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(element));
                return searchElement.Displayed;
            }
            catch
            {
                return false;
            }
        }
        //Method to get element text
        public string GetElementText(By element)
        {
            return driver.FindElement(element).Text;
        }
        //Generic method to find element 
        public IWebElement GetElement(By element)
        {
            return driver.FindElement(element);
        }
        //methos to get driver form Base Page
        public IWebDriver GetDriver()
        {
            if (driver == null)
            {
                driver = new FirefoxDriver();
            }
            return driver;
        }
        //methos to set driver to Base Page
        public void SetDriver(IWebDriver d)
        {
            driver = d;
        }
        //methos to click on menu item
        public void ClickOnMenuOption(By menuElement, By itemElement)
        {
            Assert.IsTrue(IsElementVisible(menuElement), "Menu Element is not visible on page");
            GetElement(menuElement).Click();
            Assert.IsTrue(IsElementVisible(itemElement), "Menu Element is not visible on page");
            GetElement(itemElement).Click();
        }
        //methos to wait until an element becomes invisible
        public static void WaitUntilInvisible(By element)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(element));
        }
        //methos to check Ascending Order Sorting
        public void CheckAscendingSorting(string columnName)
        {
            List<string> beforeSortingValues = GetColumnTextFromAllPages(Constants.NameColumnHeadingtext);
            beforeSortingValues.Sort();
            driver.Navigate().Refresh();
            WaitUntilInvisible(loadingSpinner);
            Assert.IsTrue(IsElementVisible(svgBtn), "Svg for sorting is not displayed");
            GetElement(svgBtn).Click();
            WaitUntilInvisible(loadingSpinner);
            Assert.IsTrue(IsElementVisible(decendingOrderSvg), "Ascending Order svg is not displayed");
            List<string> afterSortingValues = GetColumnTextFromAllPages(Constants.NameColumnHeadingtext);

            for (int j = 0; j < beforeSortingValues.Count; j++)
            {
                Assert.IsTrue(beforeSortingValues[j] == afterSortingValues[j], "Ascending Sorting Failed");
            }
        }
        //method to get list of text from list of Elements
        public List<string> GetColumnText(List<IWebElement> webElementslist)
        {
            List<string> columnTextlist = new List<string>();
            for (int i = 0; i < webElementslist.Count; i++)
            {
                columnTextlist.Add(webElementslist[i].Text);
            }
            return columnTextlist;
        }
        public List<string> GetColumnTextFromAllPages(string columnName)
        {
            //paginationInfoNumbers[0] is starting Number of row in current page
            //paginationInfoNumbers[1] is Ending row in current page
            //paginationInfoNumbers[2] is total Number of rows in table

            int columnNumber = GetColumnNumber(columnName);
            int[] paginationInfoNumbers = { 0, 0, 0 };
            int startingRowNumber = 0;
            int EndingRowNumber = 0;
            int totalRows = 0;
            List<string> columnTextList = new List<string>();
            WaitUntilInvisible(loadingSpinner);
            if (!IsElementVisible(By.XPath("//table/tbody/tr")))
            {
                List<string> li = null;
                return li;

            }
            do
            {
                FilterNumbers(ref paginationInfoNumbers);
                startingRowNumber = paginationInfoNumbers[0];
                EndingRowNumber = paginationInfoNumbers[1];
                totalRows = paginationInfoNumbers[2];
                for (int i = 1; i <= EndingRowNumber - (startingRowNumber - 1); i++)
                {
                    if (columnNumber > 2)
                    {
                        columnTextList.Add(GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[" + columnNumber + "]")).Text);
                    }
                    else
                    {
                        List<string> li = null;
                        return li;
                    }
                }
                if (EndingRowNumber < totalRows)
                {
                    Assert.IsTrue(IsElementVisible(paginationNextBtn), "Pagination button is not visible");
                    GetElement(paginationNextBtn).Click();
                    WaitUntilInvisible(loadingSpinner);
                }
            } while (EndingRowNumber < totalRows);
            return columnTextList;
        }
        //methos to check Descending Order sorting
        public void CheckDescendingSorting(string columnName)
        {
            List<string> beforeSortingValues = GetColumnTextFromAllPages(Constants.NameColumnHeadingtext);
            beforeSortingValues.Sort();
            beforeSortingValues.Reverse();
            driver.Navigate().Refresh();
            WaitUntilInvisible(loadingSpinner);
            //click on svg 
            Assert.IsTrue(IsElementVisible(svgBtn), "Svg for sorting is not displayed");
            Actions svgAction = new Actions(driver);
            svgAction.DoubleClick(GetElement(svgBtn)).Perform();
            WaitUntilInvisible(loadingSpinner);
            Assert.IsTrue(IsElementVisible(decendingOrderSvg), "Ascending Order svg is not displayed");
            List<string> afterSortingValues = GetColumnTextFromAllPages(Constants.NameColumnHeadingtext);
            for (int j = 0; j < beforeSortingValues.Count; j++)
            {
                Assert.IsTrue(beforeSortingValues[j] == afterSortingValues[j], "Ascending Sorting Failed");
            }
        }
        //method to get list of values of a specified column
        public List<IWebElement> GetColumnWebElements(string columnName)
        {
            //paginationInfoNumbers[0] is starting Number of row in current page
            //paginationInfoNumbers[1] is Ending row in current page
            //paginationInfoNumbers[2] is total Number of rows in table
            int columnNumber = GetColumnNumber(columnName);
            int[] paginationInfoNumbers = { 0, 0, 0 };
            int startingRowNumber = 0;
            int EndingRowNumber = 0;
            int totalRows = 0;
            List<IWebElement> column = new List<IWebElement>();
            Assert.IsTrue(IsElementVisible(By.XPath("//table/tbody/tr")), "Table Element is Not displayed");
            do
            {
                FilterNumbers(ref paginationInfoNumbers);
                startingRowNumber = paginationInfoNumbers[0];
                EndingRowNumber = paginationInfoNumbers[1];
                totalRows = paginationInfoNumbers[2];
                for (int i = 1; i <= EndingRowNumber - (startingRowNumber - 1); i++)
                {
                    //Assert.IsTrue(IsElementVisible(By.XPath("//div/table/tbody/tr[" + i + "]/td[" + colNumber + "]")), "//div/table/tbody/tr[" + i + "]/td[" + colNumber + "] is not displayed");
                    if (GetColumnNumber(columnName) == 1)
                    {
                        column.Add(GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[" + columnNumber + "]/input")));
                    }
                    else if (GetColumnNumber(columnName) == 2)
                    {
                        column.Add(GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[" + columnNumber + "]/div/button")));
                    }
                    else
                    {
                        column.Add(GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[" + columnNumber + "]")));
                    }
                }
                if (EndingRowNumber < totalRows)
                {
                    Assert.IsTrue(IsElementVisible(paginationNextBtn), "Pagination button is not visible");
                    GetElement(paginationNextBtn).Click();
                    WaitUntilInvisible(loadingSpinner);
                }
            } while (EndingRowNumber < totalRows);
            return column;
        }
        //method to filter numbers from pagination info list string 
        public void FilterNumbers(ref int[] num)
        {
            WaitUntilInvisible(loadingSpinner);
            Assert.IsTrue(IsElementVisible(paginationInfoList), "Pagination info list is not displayed");
            string str = GetElementText(paginationInfoList);
            int k = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char ch = 'a';
                string token = "";
                for (int j = i; ch != ' '; j++)
                {
                    if (j == str.Length)
                    {
                        break;
                    }
                    ch = str[j];
                    if (ch != ' ')
                    {

                        token += ch.ToString();
                    }
                }
                bool isDigit = int.TryParse(token, out int temp);
                if (isDigit)
                {
                    num[k] = temp;
                    k++;
                }
                i += token.Length;
            }
        }
        //method to check readonly scenario of a row
        public void CheckReadOnlyView(By element)
        {
            Actions act = new Actions(driver);
            Assert.IsTrue(IsElementVisible(By.XPath("//table/tbody/tr")), "Table row not found");
            IWebElement row = GetElement(By.XPath("//table/tbody/tr"));
            act.DoubleClick(row).Perform();
            Assert.IsTrue(IsElementVisible(element), "Modal is not display");
            Assert.IsTrue(!GetElement(updateBtn).Enabled, "Update button is Enabled");
        }
        //method to get table head row
        public IWebElement GetTableHead()
        {
            Assert.IsTrue(IsElementVisible(tableHead), "Table Head/Columns not displayed");
            return GetElement(tableHead);
        }
        //method to check 
        public void CheckOrdering()
        {
            WaitUntilInvisible(loadingSpinner);
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

            // Login
            Login();

            Assert.IsTrue(IsElementVisible(dashboardHeading), "Dashboard Heading is not displayed");
            Assert.AreEqual(GetElementText(dashboardHeading), "Dashboard", "Dashboard Text is not displayed as heading");
            Console.WriteLine("Login Successful");
            ClickOnMenuOption(PeripheralTypesPage.hardwareConfigurationMenu, PeripheralTypesPage.peripheralTypes);
            Assert.IsTrue(IsElementVisible(By.XPath("//table/thead/tr/th[3]")), "Column name is not displayed");
            string textAfter = GetElementText(By.XPath("//thead/tr/th[3]/div/span/span"));
            Assert.IsFalse(textBefore == textAfter, "Drag & drop column is not successful");
        }

        protected abstract void Login();

        //method to active a deleted record
        public void ActiveRecord(string filterName, string filterValue, string nameKey)
        {
            bool isNameMatched = false;
            Assert.IsTrue(IsElementVisible(filterBtn), "Filter button is not displayed");
            GetElement(filterBtn).Click();
            Assert.IsTrue(IsElementVisible(filterNameDropdown), "Filter Name dropdown is nnot displayed");
            GetElement(filterNameDropdown).Click();
            GetElement(By.XPath("//select[@name='field0']/option[@value='" + filterName + "']")).Click();
            GetElement(filterValueDropDown).Click();
            Assert.IsTrue(IsElementVisible(filterBtn), "Filter button is not displayed");
            GetElement(filterBtn).Click();
            Assert.IsTrue(IsElementVisible(filterNameDropdown), "Filter Name dropdown is nnot displayed");
            GetElement(filterNameDropdown).Click();
            GetElement(By.XPath("//select[@name='field0']/option[@value='" + filterName + "']")).Click();
            GetElement(filterValueDropDown).Click();
            GetElement(By.XPath("//select[@name='value0']/option[@value='" + filterValue + "']")).Click();
            GetElement(applyFilterBtn).Click();
            WaitUntilInvisible(loadingSpinner);
            List<IWebElement> nameColumnElementsList = GetColumnWebElements("Name");
            //driver.Navigate().Refresh();
            //WaitUntilInvisible(loadingSpinner);
            List<string> nameColumnValuesList = GetColumnText(nameColumnElementsList);
            List<IWebElement> editBtnList = GetColumnWebElements("Actions");
            for (int i = 0; i < nameColumnValuesList.Count; i++)
            {
                if (nameKey == nameColumnValuesList[i])
                {
                    editBtnList[i].Click();
                    isNameMatched = true;
                    break;
                }
            }
            Assert.Multiple(() =>
           {
               Assert.IsTrue(isNameMatched, "No record agianst " + nameKey);
               Assert.IsTrue(IsElementVisible(modal), "Model Not displayed");
               Assert.IsTrue(IsElementVisible(By.XPath("//form[@class='form av-valid']//div//span[text()='Is Active']")), "checkbox element is not displayed");
           }
                );
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.querySelector(\"label[for=isActive]\").click()");
            GetElement(updateBtn).Click();
            WaitUntilInvisible(loadingSpinner);
            Assert.IsTrue(IsElementVisible(toastMessage), "Verify toast message is not displayed");
        }
        //method to get column number of a column in table
        public int GetColumnNumber(string columnName)
        {
            int i = 0;
            IWebElement header = GetElement(tableHead);
            int totalColumnNumbers = header.FindElements(By.TagName("th")).Count;
            bool isColumnMatch = false;
            for (i = 1; i <= totalColumnNumbers; i++)
            {

                try
                {
                    isColumnMatch = driver.FindElement(By.XPath("//table//tr/th[" + i + "]//div//span[text()='" + columnName + "']")).Displayed;
                }
                catch
                {
                    isColumnMatch = false;
                }
                if (isColumnMatch)
                {
                    break;
                }
            }
            if (isColumnMatch)
            {
                return i;
            }
            return -1;
        }
        //method to check filter testcase
        public void CheckFilter(string filterName, string filterValue, string searchText)
        {
            WaitUntilInvisible(loadingSpinner);
            Assert.IsTrue(IsElementVisible(filterBtn), "Filter button is not displayed");
            GetElement(filterBtn).Click();
            Assert.IsTrue(IsElementVisible(filterNameDropdown), "Filter Name dropdown is nnot displayed");
            GetElement(filterNameDropdown).Click();
            GetElement(By.XPath("//select[@name='field0']/option[@value='" + filterName + "']")).Click();
            GetElement(filterTypeDropdown).Click();
            GetElement(By.XPath("//select[@name='type0']/option[@value='" + filterValue + "']")).Click();
            GetElement(By.Id("value0")).SendKeys(searchText);
            GetElement(applyFilterBtn).Click();
            WaitUntilInvisible(loadingSpinner);
            List<string> columnTextlist = GetColumnTextFromAllPages(Constants.NameColumnHeadingtext);
            switch (filterValue)
            {
                //filter case when selected value is Equal
                case Constants.FilterEqualValue:
                    CheckFilterWithEqualValue(columnTextlist, searchText);
                    break;
                //filter case when selected value is Not Equal
                case Constants.FilterNotEqualValue:
                    CheckFilterWithNotEqualValue(columnTextlist, searchText);
                    break;
                //filter case when selected value is Contains
                case Constants.FilterContainValue:
                    CheckFilterWithContainValue(columnTextlist, searchText);
                    break;
                //filter case when selected value is Not Contains
                case Constants.FilterNotContainValue:
                    CheckFilterWithNotContainValue(columnTextlist, searchText);
                    break;

            }
        }
        //method to check filter with contains value
        public void CheckFilterWithContainValue(List<string> list, string containKey)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Assert.IsTrue(Regex.IsMatch(list[0], Regex.Escape(containKey), RegexOptions.IgnoreCase));
                }
            }
        }
        //method to check filter with not contains value
        public void CheckFilterWithNotContainValue(List<string> list, string containKey)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Assert.IsTrue(!Regex.IsMatch(list[0], Regex.Escape(containKey), RegexOptions.IgnoreCase));
                }
            }
        }
        //method to check filter with equal value
        public void CheckFilterWithEqualValue(List<string> list, string containKey)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Assert.IsTrue(list[0] == containKey);
                }
            }
        }
        //method to check filter with not equal value
        public void CheckFilterWithNotEqualValue(List<string> list, string containKey)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Assert.IsTrue(list[0] != containKey);
                }
            }
        }

    }
}
