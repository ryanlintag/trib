using System.Net.Mail;

namespace Domain.ValueObjects
{
    public record Email
    {
        private Email() { }
        public Email(string address)
        {
            if (!isValid(address))
            {
                throw new DomainException("Invalid email address.");
            }
            this.Address = address;
        }
        public string Address { get; private set; } = string.Empty;
        private bool isValid(string address)
        {
            return MailAddress.TryCreate(address, out _);
        }
    }
}
