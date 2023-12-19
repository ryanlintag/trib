namespace Domain.Users
{
    public record UserRecord(string Email,
            string FirstName,
            string? MiddleName,
            string LastName,
            string Role,
            bool IsActive,
            string LastUpdatedBy,
            DateTime DateLastUpdated);
}
