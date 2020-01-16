using System.Collections.Generic;
using WebStore.Domain.DTO.Orders;

namespace WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetUserOrders(string UserName);

        OrderDTO GetOrderById(int id);

        OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName);
    }
}
