using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.WebApp
{
    public class CRUDParameters
    {
        public By AddButton { get; set; }
        public By nameTextboxInModal { get; set; }
        public By codeTextboxInModal { get; set; }
        public By modalActionButton { get; set; }
        public By modal { get; set; }
        public By toastMessaage { get; set; }
        public By pageSizingDropdown { get; set; }
        public By loadingSpinner { get; set; }
        public By paginationInfoList { get; set; }
        public By actionDropdown { get; set; }
        public By deleteButtonInActionDropdown { get; set; }
        public By confirmDeleteOkButton { get; set; }
        public By HardwareConfigurationMenu { get; set; }
        public By PeripheralTypesMenuItem { get; set; }


    }
}
