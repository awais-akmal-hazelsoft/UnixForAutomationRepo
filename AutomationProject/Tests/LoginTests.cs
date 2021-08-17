using AutomationProject.Factory;
using AutomationProject.Helper;
using NUnit.Framework;
using OpenQA.Selenium;
using UnixFor.Helper;
using Assert = NUnit.Framework.Assert;

namespace UnixFor.Tests
{
    [TestFixture]
    public class LoginTestsNew
    {
        IWebDriver _driver;

        Automation.WebApp.LoginPage _loginPage;
        Automation.WebApp.DashboardPage _dashboard;

        [OneTimeSetUp]
        public void TestInit()
        {
            _driver = Singleton.Driver;
            _loginPage = LoginPageFactory.Build();
            _dashboard = DashboardFactory.Build();
        }

        //********** Test 1 to test empty field login scenarios ***********
        [TestCase(Constants.EmptyUsername, Constants.WrongtPassword), Order(1)]
        [TestCase(Constants.WrongtUsername, Constants.EmptyPassword)]
        [TestCase(Constants.EmptyUsername, Constants.EmptyPassword)]
        public void TestLoginEmptyFieldScenario(string name, string password)
        {
            _loginPage.SetUsername(name);
            _loginPage.SetPassword(password);

            _loginPage.ClickLoginButton();

            Assert.IsTrue(_loginPage.IsToastMessageVisible(), "Toast message is not displayed");
        }
        //[TestCase("", "123"), Order(1)]
        //[TestCase("123", "")]
        //[TestCase("", "")]
        //public void TestLoginEmptyFieldScenario(string UserName, string password)
        //{
        //    loginObj.CheckLoginEmptyFieldScenarios(UserName, password);
        //}

        //**************Test 2 to test invalid credentials scenarios***************
        [TestCase(Constants.WrongtUsername, Constants.CorrectPassword), Order(1)]
        [TestCase(Constants.CorrectUsername, Constants.WrongtPassword)]
        [TestCase(Constants.WrongtUsername, Constants.WrongtPassword)]

        public void TestLoginInvalidValuesScenario(string UserName, string password)
        {
            _loginPage.SetUsername(UserName);
            _loginPage.SetPassword(password);

            _loginPage.ClickLoginButton();

            Assert.IsFalse(_dashboard.IsDashboardVisible(), "Dashboard Heading is not displayed");
        }

        [TestCase(Constants.CorrectUsername, Constants.CorrectPassword), Order(2)]
        public void TestLoginValidValuesScenario(string UserName, string password)
        {
            _loginPage.SetUsername(UserName);
            _loginPage.SetPassword(password);

            _loginPage.ClickLoginButton();

            Assert.IsTrue(_dashboard.IsDashboardVisible(), "Dashboard Heading is not displayed");
        }

        //******************Test 3 to test valid login scenario*******************
        //[TestCase("Admin", "Admin!23"), Order(3)]
        //public void TestValidLogin(string UserName, string password)
        //{
        //    loginObj.CheckValidLoginFieldScenarios(UserName, password);
        //    loginObj.SetDriver(driver);
        //}

    }
}
