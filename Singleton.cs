using OpenQA.Selenium;

namespace AutomationProject.Helper
{
    public static class Singleton
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
