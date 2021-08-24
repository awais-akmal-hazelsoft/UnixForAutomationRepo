using OpenQA.Selenium;

namespace Automation.WebApp
{
    public class Filtering : AutomationBase
    {
        private readonly FilteringParameters _parameters;

        public Filtering(IWebDriver driver, FilteringParameters parameters)
            : base(driver)
        {
            _driver = driver;
            _parameters = parameters;
        }

        public void ClickOnFilterButton()
        {
            GetElement(_parameters.FilterButton).Click();
        }

        public void ClickOnApplyFilterButton()
        {
            GetElement(_parameters.ApplyFilterButton).Click();
        }

        public void ClickOnFilterNameDropdown()
        {
            GetElement(_parameters.FilterNameDropdown).Click();
        }
        
        public void ClickOnFilterTypeDropdown()
        {
            GetElement(_parameters.FilterTypeDropdown).Click();
        }

        public void ClickOnFilterValueDropdown()
        {
            GetElement(_parameters.FilterValueDropDown).Click();
        }

        
        
    }
}
