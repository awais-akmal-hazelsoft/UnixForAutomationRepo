using NUnit.Framework;
using System;

namespace AutomationProject.Tests
{
    [TestFixture]
    public class ReadonlyViewTests
    {
        Automation.WebApp.ReadonlyView _readOnlyViewObj;
        [Test]
        public void TestReadonlyView()
        {

            _readOnlyViewObj.DoubleClickOnRowFirstRow();

            Assert.IsTrue(_readOnlyViewObj.IsModalDisplay(), "Modal is not displayed");
            Assert.IsTrue(_readOnlyViewObj.IsModalDisable(), "Modal fields are not displayed");
        }
    }
}
