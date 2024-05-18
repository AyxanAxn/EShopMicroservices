namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string State { get; } = default!;
        public string Country { get; } = default!;
        public string ZipCode { get; } = default!;
        public string LastName { get; } = default!;
        public string FirstName { get; } = default!;
        public string AddressLine { get; } = default!;
        public string EmailAddress { get; } = default!;
        protected Address() { }
        private Address(string firstName, string lastName, string state, string country, string zipCode, string addressLine, string emailAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            State = state;
            Country = country;
            ZipCode = zipCode;
            AddressLine = addressLine;
            EmailAddress = emailAddress;
        }
        public static Address Of(string firstName, string lastName, string state, string country, string zipCode, string addressLine, string emailAddress)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
            
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

            return new Address(firstName, lastName, state, country, zipCode, addressLine, emailAddress);
        }
    }
}