using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;


namespace Automation.WebApp
{
    public class AutomationBase
    {
        protected IWebDriver _driver;

        public AutomationBase(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement GetElement(By element)
        {
            return _driver.FindElement(element);
        }

        public void ClickOnButton(By element)
        {
            GetElement(element).Click();
        }

        public bool IsElementVisible(By element)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(8));

            try
            {
                IWebElement searchElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(element));
                return searchElement.Displayed;
            }
            catch
            {
                return false;
            }
        }

        public void DoubleClick(By element)
        {
            Actions act = new Actions(_driver);
            act.DoubleClick(GetElement(element));
        }

        public void SelectSidebarMenu(By menu, By option)
        {
            GetElement(menu).Click();
            GetElement(option).Click();
        }

        public void WaitUntilInvisible(By element)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(element));
        }
    }
}
