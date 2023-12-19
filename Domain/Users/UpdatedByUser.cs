using Domain.ValueObjects;

namespace Domain.Users
{
    public record UpdatedByUser
    {
        private UpdatedByUser()
        {
            
        }
        public UpdatedByUser(Email email)
        {
            this.Email = email;
        }
        public Email Email { get; private set; }
    }
}
