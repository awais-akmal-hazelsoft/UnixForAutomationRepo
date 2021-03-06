using OpenQA.Selenium;

namespace Automation.Helper
{
    public static class WebDriver
    {
        static IWebDriver _driver = null;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                    _driver = new OpenQA.Selenium.Firefox.FirefoxDriver();

                _driver.Url = "http://unixfor.hazelsoft.net/";

                return _driver;
            }
        }
    }
}
