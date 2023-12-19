namespace Presentation.RequestModels.Users
{
    public sealed record UpdateUserRequest(
            string email,
            string firstName,
            string lastName,
            string middleName,
            string role,
            bool isActive
        );
}
