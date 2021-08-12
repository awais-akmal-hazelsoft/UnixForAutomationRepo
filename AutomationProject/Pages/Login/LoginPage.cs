using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using UnixFor.Pages.Base;
using NUnit.Framework;
using UnixFor.Helper;
using System.Threading;

namespace UnixFor.Pages.Login
{
    public class LoginPage : BasePage
    {
        public static LoginPage Instance = new LoginPage();
        //button elements
        private readonly By LoginBtn = By.XPath("//button[@type='submit']");
        //text field elements
        private readonly By UserNameField = By.Name("userName");
        private readonly By PasswordField = By.Name("password");
        //warning message elements
        private readonly By userNameRequiredMessage = By.CssSelector("#userName+div");
        private readonly By passwordRequiredMessage = By.CssSelector("#password+div");

        //Function to insert login credentials into text fields       
        public void InsertLoginDetails(String name, String pwd)
        {
            IWebElement username = GetElement(UserNameField);
            username.Clear();
            username.SendKeys(name);
            IWebElement password = GetElement(PasswordField);
            password.Clear();
            password.SendKeys(pwd);
            IWebElement btnLogin = GetElement(LoginBtn);
            btnLogin.Click();
        }

        //Function to check Empty login scenarios
        public void CheckLoginEmptyFieldScenarios(string n, string p)
        {
            InsertLoginDetails(n, p);
            if (n == "" && p == "")
            {
                Assert.IsTrue(IsElementVisible(userNameRequiredMessage), "Field is required message is not displayed");
                Assert.AreEqual(Constants.ExpectedEmptyFieldValidation, GetElementText(userNameRequiredMessage), "Message Field is required is not displayed correctly");
                Assert.IsTrue(IsElementVisible(passwordRequiredMessage), "Field is required message is not displayed");
                Assert.AreEqual(Constants.ExpectedEmptyFieldValidation, GetElementText(passwordRequiredMessage), "Message Field is required is not displayed correctly");
            }
            else if (n == "" && p != "")
            {
                Assert.IsTrue(IsElementVisible(userNameRequiredMessage), "Field is required message is not displayed");
                Assert.AreEqual(Constants.ExpectedEmptyFieldValidation, GetElementText(userNameRequiredMessage), "Message Field is required is not displayed correctly");
                Assert.IsFalse(IsElementVisible(passwordRequiredMessage), "Field is required message is displayed");
            }
            else if (n != "" && p == "")
            {
                Assert.IsTrue(IsElementVisible(passwordRequiredMessage), "Field is required message is not displayed");
                Assert.AreEqual(Constants.ExpectedEmptyFieldValidation, GetElementText(passwordRequiredMessage), "Message Field is required is not displayed correctly");
                Assert.IsFalse(IsElementVisible(userNameRequiredMessage), "Field is required message is displayed");
            }
        }

        //Function to check Invalid login scenarios
        public void CheckLoginInvalidFieldScenarios(string n, string p)
        {
            InsertLoginDetails(n, p);

            if (n == "wrong" && p != "wrong")
            {
                Assert.IsTrue(IsElementVisible(toastMessage), "Error toast is not displayed");
                Assert.AreEqual(Constants.ExpectedUsernameInvalidToastMessage, GetElementText(toastMessage), "username is incorrect message is not displayed");
                //Thread.Sleep(6000);
                WaitUntilInvisible(toastMessage);
            }
            else if (n == "wrong" && p == "wrong")
            {
                Assert.IsTrue(IsElementVisible(toastMessage), "Error toast is not displayed");
                Assert.AreEqual(Constants.ExpectedUsernameInvalidToastMessage, GetElementText(toastMessage), "username is incorrect message is not displayed");
                //Thread.Sleep(6000);
                WaitUntilInvisible(toastMessage);
            }
            else if (n != "wrong" && p == "wrong")
            {
                Assert.IsTrue(IsElementVisible(toastMessage), "Error toast is not displayed");
                Assert.AreEqual(Constants.ExpectedPasswordInvalidToastMessage, GetElementText(toastMessage), "password is incorrect message is not displayed");
                //Thread.Sleep(6000);
                WaitUntilInvisible(toastMessage);
            }
        }

        //Function to check valid login scenario
        public void CheckValidLoginFieldScenarios(string n, string p)
        {
            InsertLoginDetails(n, p);
            Assert.IsTrue(IsElementVisible(dashboardHeading), "Dashboard Heading is not displayed");
            Assert.AreEqual(GetElementText(dashboardHeading), "Dashboard", "Dashboard Text is not displayed as heading");
        }

        protected override void Login()
        {
            //
        }
    }
}
