using Automation.Helper;
using Automation.WebApp;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProject.Factory
{
    class ReadonlyViewFactory
    {
        public static ReadonlyView Build()
        {
            var parameters = new ReadonlyViewParameters
            {
                FirstRow = By.CssSelector("table.react-table tr"),
                Modal = By.CssSelector("div.modal-content"),
                ModalUsernameTextbox = By.CssSelector("input[name= 'name']"),
                ModalPasswordTextbox = By.CssSelector("input[name= 'code']"),
            };
            return new ReadonlyView(WebDriver.Driver, parameters);
        }
    }
}