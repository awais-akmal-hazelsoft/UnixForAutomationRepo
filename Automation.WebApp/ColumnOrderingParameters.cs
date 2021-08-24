using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.WebApp
{
    public class ColumnOrderingParameters
    {
        public By FirstColumn { get; set; }
        public By SecondColumn { get; set; }
        public By AdminButton { get; set; }
        public By LogOutButton { get; set; }

    }
}
