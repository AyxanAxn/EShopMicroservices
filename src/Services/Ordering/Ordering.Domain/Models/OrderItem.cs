using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<Guid>
    {
        public OrderItem
            (Guid orderId,Guid productId,int quantity,decimal price)
        {
            orderId=OrderId;
            productId=ProductId;
            quantity=Quantity;
            price=Price;
        }

        public Guid OrderId { get; private set; } = default!;
        public Guid ProductId { get; private set; } = default!;
        public int Quantity{ get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
    }
}