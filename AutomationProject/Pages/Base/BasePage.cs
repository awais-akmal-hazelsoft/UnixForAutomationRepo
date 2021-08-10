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

namespace UnixFor.Pages.Base
{
    public class BasePage
    {
        protected static IWebDriver driver;
        protected By svgBtn = By.XPath("(//div[@title='Toggle SortBy'])[1]");
        protected By ascendingOrderSvg = By.XPath("//th[@class='thead tr wordbreak-audit']//*[local-name()='svg']/*[local-name()='path' and @d='M19 17H22L18 21L14 17H17V3H19M2 17H12V19H2M6 5V7H2V5M2 11H9V13H2V11Z']");
        protected By decendingOrderSvg = By.XPath("//th[@class='thead tr wordbreak-audit']//*[local-name()='svg']/*[local-name()='path' and @d='M18 21L14 17H17V7H14L18 3L22 7H19V17H22M2 19V17H12V19M2 13V11H9V13M2 7V5H6V7H2Z']");
        protected By loadingSpinner = By.CssSelector("div.spinner");
        protected By paginationInfoList = By.XPath("//*[@id='root']/div[2]/div/main/div/div[2]/div/div[5]/div/div/div[1]/div[1]/nav/ul/div/li[text()='Showing']");
        protected By paginationNextBtn = By.XPath("(//li[@class='pagination__item page-item']/button[@class='pagination__link pagination__link--arrow page-link'])[2]");
        protected By paginationSizingDropdown = By.XPath("//select[@id='exampleSelect']");
        protected By paginationSizingDropdownItem = By.XPath("//select[@id='exampleSelect' ]//option[@Value='50']");
        protected By tableHead = By.XPath("//table/thead/tr");

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
        public By toastMessage
        {
            get
            {
                return By.CssSelector("div.Toastify__toast-body");
            }
        }
        public IWebDriver GetDriver()
        {
            if (driver == null)
            {
                driver = new FirefoxDriver();
            }
            return driver;
        }
        public void SetDriver(IWebDriver d)
        {
            driver = d;
        }
        public void ClickOnMenuOption(By menuElement, By itemElement)
        {
            Assert.IsTrue(IsElementVisible(menuElement), "Menu Element is not visible on page");
            GetElement(menuElement).Click();
            Assert.IsTrue(IsElementVisible(itemElement), "Menu Element is not visible on page");
            GetElement(itemElement).Click();
        }
        public static void WaitUntilInvisible(By element)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(element));
        }
        public void CheckAscendingOrder(int colNumber)
        {
            List<string> beforeSortingValues = GetColumnText(colNumber);
            beforeSortingValues.Sort();
            driver.Navigate().Refresh();
            WaitUntilInvisible(loadingSpinner);
            Assert.IsTrue(IsElementVisible(svgBtn), "Svg for sorting is not displayed");
            GetElement(svgBtn).Click();
            WaitUntilInvisible(loadingSpinner);
            Assert.IsTrue(IsElementVisible(decendingOrderSvg), "Ascending Order svg is not displayed");
            List<string> afterSortingValues = GetColumnText(colNumber);
            for (int j = 0; j < beforeSortingValues.Count; j++)
            {
                Assert.IsTrue(beforeSortingValues[j] == afterSortingValues[j], "Ascending Sorting Failed");
            }
        }
        public void CheckDescendingOrder(int colNumber)
        {
            List<string> beforeSortingValues = GetColumnText(colNumber);
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
            List<string> afterSortingValues = GetColumnText(colNumber);
            for (int j = 0; j < beforeSortingValues.Count; j++)
            {
                Assert.IsTrue(beforeSortingValues[j] == afterSortingValues[j], "Ascending Sorting Failed");
            }
        }
        public List<string> GetColumnText(int colNumber)
        {
            //paginationInfoNumbers[0] is starting Number of row in current page
            //paginationInfoNumbers[1] is Ending row in current page
            //paginationInfoNumbers[2] is total Number of rows in table
            int[] paginationInfoNumbers = { 0, 0, 0 };
            int startingRowNumber = 0;
            int EndingRowNumber = 0;
            int totalRows = 0;
            List<string> column = new List<string>();
            Assert.IsTrue(IsElementVisible(By.XPath("//table/tbody/tr")), "Table Element is Not displayed");
            string cellText = null;
            do
            {
                FilterNumbers(ref paginationInfoNumbers);
                startingRowNumber = paginationInfoNumbers[0];
                EndingRowNumber = paginationInfoNumbers[1];
                totalRows = paginationInfoNumbers[2];
                for (int i = 1; i <= EndingRowNumber - (startingRowNumber - 1); i++)
                {
                    //Assert.IsTrue(IsElementVisible(By.XPath("//div/table/tbody/tr[" + i + "]/td[" + colNumber + "]")), "//div/table/tbody/tr[" + i + "]/td[" + colNumber + "] is not displayed");
                    cellText = GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[" + colNumber + "]")).Text;
                    column.Add(cellText);
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
        public void FilterNumbers(ref int[] num)
        {
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
    }
}
