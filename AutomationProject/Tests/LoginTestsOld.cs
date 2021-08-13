using NUnit.Framework;
using UnixFor.Pages.Login;
using UnixFor.Helper;
using Assert = NUnit.Framework.Assert;
using OpenQA.Selenium;
using UnixFor.Pages.Base;

namespace UnixFor.Tests
{

    /******************************************************************************************
        Rejected Code
    ******************************************************************************************/
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver driver;
        LoginPage loginObj = new LoginPage();

        [OneTimeSetUp]
        public void TestInit()
        {
            this.driver = loginObj.GetDriver();
            driver.Url = "http://unixfor.hazelsoft.net/";
        }

        //********** Test 1 to test empty field login scenarios ***********
        [TestCase("", "123"), Order(1)]
        [TestCase("123", "")]
        [TestCase("", "")]
        public void TestLoginEmptyFieldScenario(string UserName, string password)
        {
            loginObj.CheckLoginEmptyFieldScenarios(UserName, password);
        }

        //**************Test 2 to test invalid credentials scenarios***************
        [TestCase("wrong", "Admin!23"), Order(2)]
        [TestCase("wrong", "wrong")]
        [TestCase("admin", "wrong")]
        public void TestLoginInvalidValuesScenario(string UserName, string password)
        {


            loginObj.CheckLoginInvalidFieldScenarios(UserName, password);
        }

        //******************Test 3 to test valid login scenario*******************
        [TestCase("Admin", "Admin!23"), Order(3)]
        public void TestValidLogin(string UserName, string password)
        {
            loginObj.CheckValidLoginFieldScenarios(UserName, password);
            loginObj.SetDriver(driver);
        }
    }
}
