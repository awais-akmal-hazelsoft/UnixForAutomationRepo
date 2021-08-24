using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.WebApp
{
    public class ReadonlyView : AutomationBase
    {
        private readonly ReadonlyViewParameters _parameters;

        public ReadonlyView(IWebDriver driver, ReadonlyViewParameters parameters)
            : base(driver)
        {
            _driver = driver;
            _parameters = parameters;
        }

        public void DoubleClickOnRowFirstRow()
        {
            DoubleClick(_parameters.FirstRow);
        }

        public bool IsModalDisplay()
        {
            return IsElementVisible(_parameters.Modal);
        }

        public bool IsModalDisable()
        {
            return GetElement(_parameters.ModalUsernameTextbox).Enabled;
        }
    }
}
