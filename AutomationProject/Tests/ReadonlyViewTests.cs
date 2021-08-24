using NUnit.Framework;
using System;

namespace AutomationProject.Tests
{
    [TestFixture]
    public class ReadonlyViewTests
    {
        Automation.WebApp.ReadonlyView _readOnlyView;
        [Test]
        public void TestReadonlyView()
        {
            //_readOnlyView.SelectSidebarMenu();
            //_readOnlyView.ClickOnButton();

            _readOnlyView.DoubleClickOnRowFirstRow();

            Assert.IsTrue(_readOnlyView.IsModalDisplay(), "Modal is not displayed");
            Assert.IsTrue(_readOnlyView.IsModalDisable(), "Modal fields are not displayed");
        }
    }
}
