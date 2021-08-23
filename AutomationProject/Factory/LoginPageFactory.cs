using Automation.Helper;
using Automation.WebApp;
using OpenQA.Selenium;


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
