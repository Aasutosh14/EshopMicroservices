using Basket.API.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);
    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto is required.");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required.");
            RuleFor(x => x.BasketCheckoutDto.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
            RuleFor(x => x.BasketCheckoutDto.TotalPrice).GreaterThan(0).WithMessage("TotalPrice must be greater than 0.");
            RuleFor(x => x.BasketCheckoutDto.FirstName).NotEmpty().WithMessage("FirstName is required.");
            RuleFor(x => x.BasketCheckoutDto.LastName).NotEmpty().WithMessage("LastName is required.");
            RuleFor(x => x.BasketCheckoutDto.EmailAddress).NotEmpty().EmailAddress().WithMessage("Valid EmailAddress is required.");
            RuleFor(x => x.BasketCheckoutDto.AddressLine).NotEmpty().WithMessage("AddressLine is required.");
            RuleFor(x => x.BasketCheckoutDto.Country).NotEmpty().WithMessage("Country is required.");
            RuleFor(x => x.BasketCheckoutDto.State).NotEmpty().WithMessage("State is required.");
            RuleFor(x => x.BasketCheckoutDto.ZipCode).NotEmpty().WithMessage("ZipCode is required.");
            RuleFor(x => x.BasketCheckoutDto.CardName).NotEmpty().WithMessage("CardName is required.");
            RuleFor(x => x.BasketCheckoutDto.CardNumber).NotEmpty().CreditCard().WithMessage("Valid CardNumber is required.");
            RuleFor(x => x.BasketCheckoutDto.CardHolderName).NotEmpty().WithMessage("CardHolderName is required.");
            RuleFor(x => x.BasketCheckoutDto.Expiration).GreaterThan(DateTime.UtcNow).WithMessage("Expiration must be in the future.");
            RuleFor(x => x.BasketCheckoutDto.CVV).NotEmpty().Length(3, 4).WithMessage("CVV must be 3 or 4 digits.");
            RuleFor(x => x.BasketCheckoutDto.PaymentMethod).IsInEnum().WithMessage("PaymentMethod must be a valid enum value.");
        }
    }
    public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(request.BasketCheckoutDto.UserName, cancellationToken);
            if (basket == null)
            {
                return new CheckoutBasketResult(false);
            }
            var eventMessage = request.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();

            await publishEndpoint.Publish(eventMessage, cancellationToken);
            await repository.DeleteBasket(request.BasketCheckoutDto.UserName, cancellationToken);
            return new CheckoutBasketResult(true);
        }
    }
}
