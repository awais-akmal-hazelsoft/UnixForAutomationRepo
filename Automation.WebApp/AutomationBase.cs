using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
    }
}
