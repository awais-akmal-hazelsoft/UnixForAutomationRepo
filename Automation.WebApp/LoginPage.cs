using OpenQA.Selenium;
using System;

namespace Automation.WebApp
{
    public class LoginPage : AutomationBase
    {
        By _userName;
        By _password;
        By _loginButton;
        By _toastMessage;
        By _userNameRequiredMessage;
        By _passwordRequiredMessage;

        public LoginPage(IWebDriver driver, By userName, By password, By loginButton, By toastMessage, By userNameRequiredMessage, By passwordRequiredMessage) 
            : base(driver)
        {
            _userName = userName;
            _password = password;
            _loginButton = loginButton;
            _toastMessage = toastMessage;
            _userNameRequiredMessage = userNameRequiredMessage;
            _passwordRequiredMessage = passwordRequiredMessage;
        }

        public LoginPage(IWebDriver driver)
            : base(driver)
        {
            _driver = driver;
        }

        public void SetUsername(string userName)
        {
            _driver.FindElement(_userName).Clear();
            _driver.FindElement(_userName).SendKeys(userName);
        }

        public void SetPassword(string password)
        {
            _driver.FindElement(_password).Clear();
            _driver.FindElement(_password).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            _driver.FindElement(_loginButton).Click();
        }

        public bool IsToastMessageVisible()
        {
            return IsElementVisible(_toastMessage);
        }

        public bool IsUsernameRequiredMessageVisible()
        {
            return IsElementVisible(_userNameRequiredMessage);
        }
        
        public bool IsPasswordRequiredMessageVisible()
        {
            return IsElementVisible(_passwordRequiredMessage);
        }
    }
}
