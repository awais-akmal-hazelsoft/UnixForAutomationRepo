using Automation.Helper;
using Automation.WebApp;
using OpenQA.Selenium;

namespace AutomationProject.Factory
{
    public class FilteringFactory
    {
        public static Filtering Build()
        {
            var parameters = new FilteringParameters
            {
                FilterButton = By.XPath("//button[./span[text()='Filter']]"),
                ApplyFilterButton = By.XPath("//button[text()='Apply Filter']"),
                FilterNameDropdown = By.Name("field0"),
                FilterTypeDropdown = By.Name("type0"),
                FilterValueDropDown = By.Name("value0"),
            };
            return new Filtering(Singleton.Driver, parameters);
        }
    }
}
