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
            By userNameResquiredMessage = By.CssSelector("#userName+div");
            By passwordRequiredMessage = By.CssSelector("#password+div");

            return new LoginPage(WebDriver.Driver, userName, passwordField, loginBtn, toastMessage, userNameResquiredMessage, passwordRequiredMessage);
        }
    }
}
