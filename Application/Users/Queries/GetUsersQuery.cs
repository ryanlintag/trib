using Domain.Users;
using Mediator;

namespace Application.Users.Queries
{
    public sealed record GetUsersQuery(string? searchValue, int? pageNumber, int? pageSize, string? sortProperty, string? sortOrder) : SearchQuery(searchValue, pageNumber, pageSize, sortProperty, sortOrder), IRequest<PagedList<UserRecord>>;
       
}
