using Application.UseCases.Users;
using Mediator;

namespace api.Endpoints
{
    public sealed class UserEndpoint : BaseEndpoint
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/users");

            group.MapGet("", GetAllUsers);
        }

        internal async Task<IResult> GetAllUsers(IMediator mediator)
        {
            var request = new GetAllUsersListRequest();
            var result = await mediator.Send(request);
            return Results.Ok(result);
        }
    }
}
