using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.WebApp
{
    public class GenericPageParameters
    {
        public By HardwareConfigurationMenu { get; set; }
        public By PeripheralTypesMenuItem { get; set; }
        public By FirstRow { get; set; }
        public By Modal { get; set; }
        public By ModalUsernameTextbox { get; set; }
        public By ModalPasswordTextbox { get; set; }

    }
}
