using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.WebApp
{
    public class GenericPage : AutomationBase
    {
        private readonly GenericPageParameters _parameters;

        public GenericPage(IWebDriver driver, GenericPageParameters parameters)
            : base(driver)
        {
            _driver = driver;
            _parameters = parameters;
        }

        public void SelectMenuOption(By menuOption)
        {
            GetElement(menuOption).Click();
        }

        public void SelectMenuItemOption(By menuItemOption)
        {
            GetElement(menuItemOption).Click();
        }

        public void DoubleClickOnRowFirstRow()
        {
            DoubleClick(_parameters.FirstRow);
        }

        public bool IsModalDisplay()
        {
            return IsElementVisible(_parameters.Modal);
        }

        public bool IsModalDisable()
        {
            return GetElement(_parameters.ModalUsernameTextbox).Enabled;
        }
    }
}
