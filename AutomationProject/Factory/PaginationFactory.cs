using Automation.Helper;
using Automation.WebApp;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProject.Factory
{
    class PaginationFactory
    {
        public static Pagination Build()
        {
            var parameters = new PaginationParameters
            {
                PaginationNextButton = By.XPath("(//li[@class='pagination__item page-item']/button[@class='pagination__link pagination__link--arrow page-link'])[2]"),
                paginationSizingDropdown = By.XPath("//select[@id='exampleSelect']"),
                paginationSizingDropdownItem = By.XPath("//select[@id='exampleSelect' ]//option[@Value='50']"),
        };


            return new Pagination(WebDriver.Driver, parameters);
        }
    }
}
