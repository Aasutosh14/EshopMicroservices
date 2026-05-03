namespace Basket.API.Dtos
{
    public class BasketCheckoutDto
    {
        public string UserName { get; set; } = default!;
        public Guid CustomerId { get; set; } = default!;
        public decimal TotalPrice { get; set; } = default!;
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string EmailAddress { get; } = default!;
        public string AddressLine { get; } = default!;
        public string Country { get; } = default!;
        public string State { get; } = default!;
        public string ZipCode { get; } = default!;
        public string CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string CardHolderName { get; } = default!;
        public DateTime Expiration { get; }
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;
    }
}
