using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
