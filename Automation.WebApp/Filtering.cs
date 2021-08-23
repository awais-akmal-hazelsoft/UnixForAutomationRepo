using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void ClickOnColumnNameDropdown()
        {
            GetElement(_parameters.FilterNameDropdown).Click();
        }

        
    }
}
