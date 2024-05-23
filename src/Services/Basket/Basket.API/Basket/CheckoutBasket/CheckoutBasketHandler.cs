using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCeckoutDto BasketCeckoutDto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);
    public class CheckoutBasketCommandValidator
        : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCeckoutDto).NotNull().WithMessage("BasketCeckoutDto can't be null");
            RuleFor(x => x.BasketCeckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }

    public class CheckoutBasketHandler
        (IBasketRepository repository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(command.BasketCeckoutDto.UserName, cancellationToken);

            if (basket is null)
                return new CheckoutBasketResult(false);

            var eventMessage = command.BasketCeckoutDto.Adapt<BasketCheckoutEvent>();

            eventMessage.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(eventMessage);

            await repository.DeleteBasket(command.BasketCeckoutDto.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}