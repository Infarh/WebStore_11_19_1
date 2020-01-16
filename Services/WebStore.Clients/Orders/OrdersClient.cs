using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.DTO.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration Configuration) : base(Configuration, "api/Orders") { }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => Get<List<OrderDTO>>($"{_ServiceAddress}/user/{UserName}");

        public OrderDTO GetOrderById(int id) => Get<OrderDTO>($"{_ServiceAddress}/{id}");

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName) => Post($"{_ServiceAddress}/{UserName}", OrderModel)
           .Content
           .ReadAsAsync<OrderDTO>()
           .Result;
    }
}
