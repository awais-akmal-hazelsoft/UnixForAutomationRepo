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
    public static class LoginPageFactory
    {
        public static LoginPage Build()
        {
            By userName = By.Name("userName");
            By passwordField = By.Name("password");
            By loginBtn = By.XPath("//button[@type='submit']");
            By toastMessage =  By.CssSelector("div.Toastify__toast-body");

            return new LoginPage(Singleton.Driver, userName, passwordField, loginBtn, toastMessage);
        }
    }
}
