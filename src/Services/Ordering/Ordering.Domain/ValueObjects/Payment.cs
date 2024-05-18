namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        private const int _lenght = 3;
        public string? CardName { get; } = default!;
        public string CardNumber { get; set; } = default!;
        public int PaymentMethod { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;
        protected Payment()
        {

        }
        private Payment(string? cardName, string cardNumber, int paymentMethod, string expiration, string cvv)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            PaymentMethod = paymentMethod;
            Expiration = expiration;
            CVV = cvv;
        }
        public static Payment Of(string? cardName, string cardNumber, int paymentMethod, string expiration, string cvv)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);

            ArgumentException.ThrowIfNullOrWhiteSpace(cardName);

            ArgumentException.ThrowIfNullOrWhiteSpace(cvv);

            ArgumentOutOfRangeException.ThrowIfNotEqual(cvv.Length, _lenght);

            return new Payment(cardName, cardNumber, paymentMethod, expiration, cvv);
        }
    }
}