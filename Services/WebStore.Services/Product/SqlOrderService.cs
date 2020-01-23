using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Product
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreContext _db;
        private readonly UserManager<User> _UserManager;
        private readonly Logger<SqlOrderService> _Logger;

        public SqlOrderService(WebStoreContext db, UserManager<User> UserManager, Logger<SqlOrderService> Logger)
        {
            _db = db;
            _UserManager = UserManager;
            _Logger = Logger;
        }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => _db.Orders
           .Include(order => order.User)
           .Include(order => order.OrderItems)
           .Where(order => order.User.UserName == UserName)
           .ToArray()
           .Select(o => new OrderDTO
            {
                Phone = o.Phone,
                Address = o.Address,
                Date = o.Date,
                OrderItems = o.OrderItems.Select(item => new OrderItemDTO
                {
                    Id = item.Id,
                    Price = item.Price,
                    Quantity = item.Quantity
                })
            });

        public OrderDTO GetOrderById(int id)
        {
            var o = _db.Orders
               .Include(order => order.OrderItems)
               .FirstOrDefault(order => order.Id == id);
            return o is null ? null : new OrderDTO
            {
                Phone = o.Phone,
                Address = o.Address,
                Date = o.Date,
                OrderItems = o.OrderItems.Select(item => new OrderItemDTO
                {
                    Id = item.Id,
                    Price = item.Price,
                    Quantity = item.Quantity
                })
            };
        }

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName)
        {
            var user = _UserManager.FindByNameAsync(UserName).Result;

            using (var transaction = _db.Database.BeginTransaction())
            {
                var order = new Order
                {
                    Name = OrderModel.OrderViewModel.Name,
                    Address = OrderModel.OrderViewModel.Address,
                    Phone = OrderModel.OrderViewModel.Phone,
                    User = user,
                    Date = DateTime.Now
                };

                _db.Orders.Add(order);

                foreach (var item in OrderModel.OrderItems)
                {
                    var product = _db.Products.FirstOrDefault(p => p.Id == item.Id);
                    if(product is null)
                        throw new InvalidOperationException($"Товар с идентификатором id:{item.Id} отсутствует в БД");

                    var order_item = new OrderItem
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = item.Quantity,
                        Product = product
                    };

                    _db.OrderItems.Add(order_item);
                }

                _db.SaveChanges();
                transaction.Commit();
                return new OrderDTO
                {
                    Phone = order.Phone,
                    Address = order.Address,
                    Date = order.Date,
                    OrderItems = order.OrderItems.Select(item => new OrderItemDTO
                    {
                        Id = item.Id,
                        Price = item.Price,
                        Quantity = item.Quantity
                    })
                };
            }
        }
    }
}
