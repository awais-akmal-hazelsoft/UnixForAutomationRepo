using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.WebApp
{
    public class CRUD : AutomationBase
    {
        private readonly CRUDParameters _parameters;

        public CRUD(IWebDriver driver, CRUDParameters parameters)
            :base(driver)
        {
            _parameters = parameters;
        }

    }
}
