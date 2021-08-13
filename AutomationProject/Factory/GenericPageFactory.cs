using Automation.WebApp;
using AutomationProject.Helper;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProject.Factory
{
    public static class GenericPageFactory
    {
        public static GenericPage Build()
        {
            var parameters = new GenericPageParameters
            {
                HardwareConfigurationMenu = By.XPath("//button[p[@class= 'sidebar__link-title' and text() = 'Hardware Configuration' ]]"),
                PeripheralTypesMenuItem = By.CssSelector("a[href='/peripheralsTypes']"),
                FirstRow = By.CssSelector("table.react-table tr"),
                Modal = By.CssSelector("div.modal-content"),
                ModalUsernameTextbox = By.CssSelector("input[name= 'name']"),
                ModalPasswordTextbox = By.CssSelector("input[name= 'code']"),
        };

            return new GenericPage(Singleton.Driver, parameters);
        }
    }
}
