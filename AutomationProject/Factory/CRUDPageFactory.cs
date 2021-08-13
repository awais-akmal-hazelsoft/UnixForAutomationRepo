using Automation.WebApp;
using AutomationProject.Helper;
using OpenQA.Selenium;

namespace AutomationProject.Factory
{
    public static class CRUDPageFactory
    {
        public static CRUD Build()
        {
            var parameters = new CRUDParameters
            {
                AddButton = By.XPath("//button[./span[text()='Add New']]"),
            };

            return new CRUD(Singleton.Driver, parameters);
        }
    }
}
