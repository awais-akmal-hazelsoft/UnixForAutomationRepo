using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

using Automation.Helper;

namespace Automation.WebApp
{
    public class ColumnOrdering : AutomationBase
    {
        private readonly ColumnOrderingParameters _parameters;

        public ColumnOrdering(IWebDriver driver, ColumnOrderingParameters parameters)
            : base(driver)
        {
            _driver = driver;
            _parameters = parameters;
        }

        public void DragColumn()
        {
            IWebElement from = GetElement(_parameters.FirstColumn);
            IWebElement to =  GetElement(_parameters.SecondColumn);
            Actions builder = new Actions(_driver);
            builder.ClickAndHold(from).Perform();
            builder.Release(to).Perform();
        }

        public void LogOut()
        {
            GetElement(_parameters.AdminButton).Click();
            GetElement(_parameters.LogOutButton).Click();
        }

        public void LogIn()
        {
            LoginPage loginPageObj = new LoginPage(_driver);
            
            loginPageObj.SetUsername(Constants.CorrectUsername);
            loginPageObj.SetPassword(Constants.CorrectPassword);

            loginPageObj.ClickLoginButton();
        }


    }
}
