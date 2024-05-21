using Ordering.Application.Exceptions;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using BuildingBlocks.CQRS;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler
        (IApplicationDbContext context)
        : ICommandHandler<UpdateOrderCommand, UpdateOrderCommandResult>
    {
        public async Task<UpdateOrderCommandResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await context
                    .Orders
                    .FindAsync(OrderId.Of(command.Order.Id), cancellationToken);
            if (order is null)
                throw new OrderNotFoundException(command.Order.Id);

            UpdateOrderWithNewValues(order, command.Order);

            context.Orders.Update(order);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateOrderCommandResult(true);
        }

        private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
        {
            var updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.BillingAddress.ZipCode);
            var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
            var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            order.Update(orderName: OrderName.Of(orderDto.OrderName),
               shippingAddress: updatedShippingAddress,
               billingAddress: updatedBillingAddress,
               payment: updatedPayment,
               status: orderDto.Status);

        }
    }
}
