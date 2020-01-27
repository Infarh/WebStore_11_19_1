using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WebStore.Controllers;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Blog();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.BlogSingle();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ContactUs_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.ContactUs();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Error404_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Error404();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ErrorStatus_404_Redirect_to_Error404()
        {
            var controller = new HomeController();
            
            var result = controller.ErrorStatus("404");

            var redirect_to_action = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirect_to_action.ControllerName);
            Assert.Equal(nameof(HomeController.Error404), redirect_to_action.ActionName);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void ThrowException_throw_ApplicationException()
        {
            var controller = new HomeController();

            var result = controller.ThrowException();
        }
    }
}
