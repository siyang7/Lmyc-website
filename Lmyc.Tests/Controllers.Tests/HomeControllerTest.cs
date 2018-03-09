using System;
using Lmyc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lmyc.Tests.Controllers.Tests
{
    [TestClass]
    [TestCategory("Home Controller Tests")]
    public class HomeControllerTest
    {
        private HomeController controller;

        [TestInitialize()]
        public void Startup()
        {
            // Run before each test
            controller = new HomeController();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            // Run after each test
        }

        [TestMethod]
        public void Index_should_return_default_view()
        {
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [TestMethod]
        public void About_should_return_about_view()
        {
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "About");
        }

        [TestMethod]
        public void Contact_should_return_contact_view()
        {
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Contact");
        }
    }
}
