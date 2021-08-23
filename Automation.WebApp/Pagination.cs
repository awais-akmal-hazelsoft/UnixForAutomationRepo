using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.WebApp
{
    public class Pagination : AutomationBase
    {
        private readonly PaginationParameters _parameters;

        public Pagination(IWebDriver driver, PaginationParameters parameters)
            : base(driver)
        {
            _driver = driver;
            _parameters = parameters;
        }

        public By GetPaginationNextButton()
        {
            return _parameters.PaginationNextButton;
        }
        
        public By GetPaginationSizingDropdown()
        {
            return _parameters.paginationSizingDropdown;
        }
        
        public By GetPaginationSizingDropdownItem()
        {
            return _parameters.paginationSizingDropdownItem;
        }

    }
}
