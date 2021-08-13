using OpenQA.Selenium;

namespace Automation.WebApp
{
    public class LoginPage : AutomationBase
    {
        By _userName;
        By _password;
        By _loginButton;

        public LoginPage(IWebDriver driver, By userName, By password, By loginButton) 
            : base(driver)
        {
            _userName = userName;
            _password = password;
            _loginButton = loginButton;
        }

        public void SetUsername(string userName)
        {
            _driver.FindElement(_userName).Clear();
            _driver.FindElement(_userName).SendKeys(userName);
        }

        public void SetPassoword(string password)
        {
            _driver.FindElement(_password).Clear();
            _driver.FindElement(_password).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            _driver.FindElement(_loginButton).Click();
        }
    }
}
