using Automation.Helper;
using Automation.WebApp;
using OpenQA.Selenium;

namespace AutomationProject.Factory
{
    public static class CRUDFactory
    {
        public static CRUD Build()
        {
            var parameters = new CRUDParameters
            {
                AddButton = By.XPath("//button[./span[text()='Add New']]"),
                nameTextboxInModal = By.CssSelector("input[name='name']"),
                codeTextboxInModal = By.CssSelector("input[name='code']"),
                modalActionButton = By.CssSelector("div.form__form-group>button"),
                modal = By.CssSelector("div.modal-content"),
                toastMessaage = By.CssSelector("div.Toastify__toast-body"),
                pageSizingDropdown = By.CssSelector("select[id='exampleSelect']"),
                loadingSpinner = By.CssSelector("div.spinner"),
                paginationInfoList = By.CssSelector("nav.pagination>ul li.pagination-info"),
                actionDropdown = By.XPath("//div[@class='dropdown'][2]/button"),
                deleteButtonInActionDropdown = By.XPath("//button[text()='Delete']"),
                confirmDeleteOkButton = By.CssSelector("div>button.modal_ok"),
                HardwareConfigurationMenu = By.XPath("//button[p[@class= 'sidebar__link-title' and text() = 'Hardware Configuration' ]]"),
                PeripheralTypesMenuItem = By.CssSelector("a[href='/peripheralsTypes']"),
            };

            return new CRUD(WebDriver.Driver, parameters);
        }
    }
}
