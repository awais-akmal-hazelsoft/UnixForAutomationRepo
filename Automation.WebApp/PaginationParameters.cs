using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.WebApp
{
    public class PaginationParameters
    {
        public By PaginationNextButton { get; set; }
        public By paginationSizingDropdown { get; set; }
        public By paginationSizingDropdownItem { get; set; }
         
    }
}
