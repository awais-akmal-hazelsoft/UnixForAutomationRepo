using Amazon.DynamoDBv2;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.WebApp
{
    public class CRUD : AutomationBase
    {
        private readonly CRUDParameters _parameters;

        public CRUD(IWebDriver driver, CRUDParameters parameters)
            :base(driver)
        {
            _driver = driver;
            _parameters = parameters;
        }

        public void ClickOnAddButton()
        {
            ClickOnButton(_parameters.AddButton);
        }

        public void ClickOnModalActionButton()
        {
            GetElement(_parameters.modalActionButton).Click();
        }

        public void SetValueInNameTextbox(string value)
        {
            GetElement(_parameters.nameTextboxInModal).SendKeys(value);
        }
        
        public void SetValueInCodeTextbox(string value)
        {
            GetElement(_parameters.codeTextboxInModal).SendKeys(value);
        }

        public bool IsModalVisible()
        {
            return IsElementVisible(_parameters.modal);
        }

        public bool IsToastMessageVisible()
        {
            return IsElementVisible(_parameters.toastMessaage);
        }

        public void WaitUntilInvisible(By element)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(element));
        }

        public void MatchingRecordForEdit(string nameKey)
        {      
            int[] pageInfoNumbers = {0, 0, 0};
            FilterNumbers(ref pageInfoNumbers); 

            for (int i = pageInfoNumbers[0]; i <= pageInfoNumbers[1]; i++)
            {
                IWebElement btnEdit = GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[2]/div/button"));
                IWebElement name = GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[3]/span/span"));
                IWebElement code = GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[4]/span/span"));
                
                if (name.Text == nameKey)
                {
                    ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].innerText=''", name);
                    ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].innerText=''", code);
                    btnEdit.Click();
                    break;
                }
            }
        }

        public void SelectRecordForDelete(string nameKey)
        {
            int[] pageInfoNumbers = { 0, 0, 0 };
            FilterNumbers(ref pageInfoNumbers);

            for (int i = pageInfoNumbers[0]; i <= pageInfoNumbers[1]; i++)
            {
                IWebElement checkbox = GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[1]/div/button"));
                IWebElement name = GetElement(By.XPath("//div/table/tbody/tr[" + i + "]/td[3]/span/span"));
                
                if (name.Text == nameKey)
                {
                    checkbox.Click();
                    break;
                }
            }
        }

        public void FilterNumbers(ref int[] num)
        {
            WaitUntilInvisible(_parameters.loadingSpinner);
           
            string str = GetElement(_parameters.paginationInfoList).Text;
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

        public void ClearNameTextbox()
        {
            GetElement(_parameters.nameTextboxInModal).Clear();
        }
        
        public void ClearCodeTextbox()
        {
            GetElement(_parameters.codeTextboxInModal).Clear();
        }

        public void ClickOnActionDropdown()
        {
            ClickOnButton(_parameters.actionDropdown);
        }

        public void ClickOnDeleteInActionDropdown()
        {
            ClickOnButton(_parameters.deleteButtonInActionDropdown);
        }

        public void ClickOnDeleteOkButton()
        {
            GetElement(_parameters.confirmDeleteOkButton).Click();
        }

        public void SelectMenuOption()
        {
            SelectSidebarMenu(_parameters.HardwareConfigurationMenu, _parameters.PeripheralTypesMenuItem);
        }
    
    }
}
