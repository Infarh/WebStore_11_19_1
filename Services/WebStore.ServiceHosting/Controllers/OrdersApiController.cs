using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/orders")]
    //[Route("api/[controller]")]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;

        public OrdersApiController(IOrderService OrderService) => _OrderService = OrderService;

        [HttpGet("user/{UserName}")]
        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => _OrderService.GetUserOrders(UserName);

        [HttpGet("{id}"), ActionName("Get")]
        public OrderDTO GetOrderById(int id) => _OrderService.GetOrderById(id);

        [HttpPost("{UserName?}")]
        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName) => _OrderService.CreateOrder(OrderModel, UserName);
    }
}