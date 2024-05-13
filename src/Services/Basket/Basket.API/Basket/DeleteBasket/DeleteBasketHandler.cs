
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName)
        : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandValudator
        : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValudator()
        {
            RuleFor(x => x.UserName)
              .NotEmpty()
              .WithMessage("UserName is required");
        }
    }
    public class DeleteBasketCommandHandler
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            return new DeleteBasketResult(true);
        }
    }
}