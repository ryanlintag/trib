namespace Presentation.RequestModels.Users
{
    public sealed record CreateUserRequest(
            string email,
            string firstName,
            string lastName,
            string middleName,
            string role
        );
}
