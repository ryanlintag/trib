using Application.Repositories;
using Domain.Users;
using Mediator;

namespace Application.Users.Queries
{
    public sealed class GetUsersQueryRequestHandler : IRequestHandler<GetUsersQuery, PagedList<UserRecord>>
    {
        private IUserRepository _repository { get; set; }
        public GetUsersQueryRequestHandler(IUserRepository repository)
        {
            this._repository = repository;
        }

        public async ValueTask<PagedList<UserRecord>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetUsers(request.SearchValue, request.PageNumber, request.PageSize, request.SortProperty, request.SortOrder, cancellationToken);
            return result;
        }
    }
}
