using Ordering.Application.Dtos;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order)
        :ICommand<UpdateOrderCommandResult>;
    public record UpdateOrderCommandResult(bool IsSuccess);
    public class UpdateOrderCommandValidator
        :AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required!");
            RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required!");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems should be not empty");
        }
    }
}