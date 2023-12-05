using RazorClassLibrary.ViewModels;

namespace RazorClassLibrary.Services
{
    public interface IUserService
    {
        List<GetUserListItemViewModel> GetUsers();
    }
    public class UsersService : IUserService
    {
        public List<GetUserListItemViewModel> GetUsers()
        {
            return new List<GetUserListItemViewModel>();
        }
    }
}
