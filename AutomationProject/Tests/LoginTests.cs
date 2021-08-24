using AutomationProject.Factory;
using NUnit.Framework;
using OpenQA.Selenium;
using Automation.Helper;
using Assert = NUnit.Framework.Assert;
using MbUnit.Framework;

namespace UnixFor.Tests
{
    
    [NUnit.Framework.TestFixture, Order(1)]
    
    public class LoginTests
    {
        IWebDriver _driver;

        Automation.WebApp.LoginPage _loginPage;
        Automation.WebApp.DashboardPage _dashboard;

        [OneTimeSetUp]
        public void TestInit()
        {
            _driver = WebDriver.Driver;
            _loginPage = LoginPageFactory.Build();
            _dashboard = DashboardFactory.Build();
        }

        //********** Test 1: to test empty field login scenarios ***********
        
        [TestCase(Constants.EmptyUsername, Constants.WrongtPassword), DependsOnAttribute(""), Order(3)]
        [TestCase(Constants.WrongtUsername, Constants.EmptyPassword)]
        [TestCase(Constants.EmptyUsername, Constants.EmptyPassword)]
        public void TestLoginEmptyFieldScenario(string name, string password)
        {
            _loginPage.SetUsername(name);
            _loginPage.SetPassword(password);

            _loginPage.ClickLoginButton();

            Assert.IsFalse(_loginPage.IsUsernameRequiredMessageVisible() || _loginPage.IsPasswordRequiredMessageVisible(), "Toast message is not displayed");
        }

        //**************Test 2: to test invalid credentials scenarios***************
     
        [TestCase(Constants.WrongtUsername, Constants.CorrectPassword), DependsOnAttribute("TestLoginEmptyFieldScenario()"), Order(2)]
        [TestCase(Constants.CorrectUsername, Constants.WrongtPassword)]
        [TestCase(Constants.WrongtUsername, Constants.WrongtPassword)]

        public void TestLoginInvalidValuesScenario(string UserName, string password)
        {
            _loginPage.SetUsername(UserName);
            _loginPage.SetPassword(password);

            _loginPage.ClickLoginButton();

            Assert.IsFalse(_dashboard.IsDashboardVisible(), "Dashboard Heading is not displayed");
        }

        //**************Test 3: to test invalid credentials scenarios***************

        [TestCase(Constants.CorrectUsername, Constants.CorrectPassword), DependsOnAttribute("TestLoginInvalidValuesScenario()"), Order(1)]
        public void TestLoginValidValuesScenario(string UserName, string password)
        {
            _loginPage.SetUsername(UserName);
            _loginPage.SetPassword(password);

            _loginPage.ClickLoginButton();

            Assert.IsTrue(_dashboard.IsDashboardVisible(), "Dashboard Heading is not displayed");
        }
    }
}
