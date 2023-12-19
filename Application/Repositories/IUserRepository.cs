using Domain.Users;
using Domain.ValueObjects;

namespace Application.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task Update(User user);
        bool UserExists(string address);
        Task CreateNewUser(User user);
        Task<User> GetUserById(UserId userId);
        Task<User> GetUserByEmail(Email email);
        Task ActivateUser(User user);
        Task DeactivateUser(User user);
        Task<PagedList<UserRecord>> GetUsers(
                string searchValue, 
                int currentPage, 
                int itemsPerPage, 
                string orderByProperty, 
                SortOrder sortOrder, 
                CancellationToken cancellationToken);
    }
}
