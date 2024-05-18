namespace Ordering.Domain.ValueObjects
{
    public class CustomerId
    {
        public Guid Value { get; }
        private CustomerId(Guid value) => value = Value;
        public static CustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
                throw new DomainException("CustomerId can't be empty");

            return new CustomerId(value);
        }
    }
}