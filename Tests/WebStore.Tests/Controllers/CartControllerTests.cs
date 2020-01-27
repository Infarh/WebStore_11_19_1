using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void CheckOut_ModelState_Invalid_Returns_ViewModel()
        {
            var cart_service_mock = new Mock<ICartService>();
            var order_service_mock = new Mock<IOrderService>();

            var controller = new CartController(cart_service_mock.Object);

            controller.ModelState.AddModelError("error", "InvalidModel");

            const string expected_model_name = "Test order";

            var result = controller.CheckOut(new OrderViewModel { Name = expected_model_name }, order_service_mock.Object);

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<DetailsViewModel>(view_result.Model);

            Assert.Equal(expected_model_name, model.OrderViewModel.Name);
        }

        [TestMethod]
        public void CheckOut_Calls_Service_and_Return_Redirect()
        {
            var cart_service_mock = new Mock<ICartService>();
            cart_service_mock
               .Setup(c => c.TransformFromCart())
               .Returns(() => new CartViewModel
                {
                    Items = new Dictionary<ProductViewModel, int>
                    {
                        { new ProductViewModel(), 1 }
                    }
                });

            const int expected_order_id = 1;

            var order_service_mock = new Mock<IOrderService>();
            order_service_mock
                .Setup(c => c.CreateOrder(It.IsAny<CreateOrderModel>(), It.IsAny<string>()))
                .Returns(new OrderDTO { Id = expected_order_id });

            var user = new ClaimsPrincipal(new ClaimsIdentity(new []{ new Claim(ClaimTypes.NameIdentifier, "1") }));
            var controller = new CartController(cart_service_mock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = user
                    }
                }
            };

            var result = controller.CheckOut(new OrderViewModel
            {
                Name = "Test",
                Address = "Test address",
                Phone = "Test phone"
            }, order_service_mock.Object);

            var redirect_result = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirect_result.ControllerName);
            Assert.Equal(nameof(CartController.OrderConfirmed), redirect_result.ActionName);

            Assert.Equal(expected_order_id, redirect_result.RouteValues["id"]);
        }
    }
}
