using Domain.Abstractions;

namespace Domain.Users
{
    public static class UserErrors
    {
        public static readonly DomainError InvalidEmail = new("User.InvalidEmail", "Invalid email set");
    }
}
