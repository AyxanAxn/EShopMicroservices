namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int _defaultLenght = 5;
        public string Value { get; }
        private OrderName(string value) => value = Value;

        public static OrderName Of(string value)
        {

            ArgumentException.ThrowIfNullOrEmpty(value);
            
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, _defaultLenght);

            return new OrderName(value);
        }
    }
}