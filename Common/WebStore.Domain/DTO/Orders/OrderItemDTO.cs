using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.DTO.Orders
{
    public class OrderItemDTO : BaseEntity
    {
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}