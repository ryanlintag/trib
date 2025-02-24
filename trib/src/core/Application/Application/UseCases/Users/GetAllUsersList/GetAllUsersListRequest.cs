using ErrorOr;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Users
{
    public sealed record GetAllUsersListRequest : IRequest<ErrorOr<List<UserListModel>>>;
    public sealed class GetAllUsersListRequestHandler : BaseRequestHandler<GetAllUsersListRequest, List<UserListModel>>
    {
        public GetAllUsersListRequestHandler(ILogger<GetAllUsersListRequestHandler> logger) : base(logger)
        {
        }
        public override async Task<ErrorOr<List<UserListModel>>> HandleRequest(GetAllUsersListRequest request, CancellationToken cancellationToken)
        {
            return new List<UserListModel>
            {
                new UserListModel(Guid.NewGuid(), "peter@parker.org", "Peter Parker"),
                new UserListModel(Guid.NewGuid(), "heero@gundamwing.org", "Heero Yuy"),
                new UserListModel(Guid.NewGuid(), "tommy@tomtoms.com", "Tommy Ranger"),
                new UserListModel(Guid.NewGuid(), "luffy@onepiece.ai", "Monkey D. Luffy"),
            };
        }
    }
    public sealed record UserListModel(Guid Id, string Email, string FullName);
}
