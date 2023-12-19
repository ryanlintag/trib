using Application;
using Application.Users.Commands;
using Application.Users.Queries;
using Domain.Users;
using Domain.Users.Events;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Infrastructure;
using Presentation.RequestModels.Users;

namespace Presentation.EndpointDefinitions
{
    public sealed class UserEndpointDefinition : IEndpointDefinition
    {
        private readonly string basePattern = "/users";
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet(basePattern, GetUsers);
            app.MapPost(basePattern, CreateUser);
            app.MapPut(basePattern + "/{userId}", UpdateUser);
            app.MapPatch(basePattern + "/{userId}/activate", ActivateUser);
            app.MapPatch(basePattern + "/{userId}/deactivate", DeactivateUser);
        }

        public void DefineServices(IServiceCollection services)
        {

        }

        public async Task<PagedList<UserRecord>> GetUsers(ISender sender, 
            [FromQuery] string? searchValue, 
            [FromQuery] int? page, 
            [FromQuery] int? pageSize, 
            [FromQuery] string? orderBy, 
            [FromQuery] string? orderType)
        {
            var query = new GetUsersQuery(searchValue, page, pageSize, orderBy, orderType);
            var result = await sender.Send(query);
            return result;
        }
        public async Task<IResult> CreateUser(ISender sender, [FromBody] CreateUserRequest request)
        {
            var updatedBy = "ryanlintag@gmail.com";
            var createRequest = new CreateUserRequestCommand(request.email, request.firstName, request.middleName, request.lastName, request.role, updatedBy);
            var result = await sender.Send(createRequest);
            if(result.IsSuccess)
            {
                return Results.Ok();
            }
            else
            {
                return result.ToProblemDetails();
            }
        }
        public async Task<IResult> UpdateUser(HttpContext context, ISender sender, [FromRoute] Guid userId, [FromBody] UpdateUserRequest request)
        {
            //var updatedBy = context.User.Identity.Name;
            var updatedBy = "ryanlintag@gmail.com";
            var updateCommand = new UpdateUserRequestCommand(userId, request.email, request.firstName, request.lastName, request.middleName, request.role, request.isActive, updatedBy);
            var result = await sender.Send(updateCommand);
            if(result.IsSuccess)
            {
                return Results.Ok();
            }
            else 
            {
                return result.ToProblemDetails();
            }
        }
        public async Task<IResult> ActivateUser(HttpContext context, ISender sender, [FromRoute] Guid userId)
        {
            var updatedBy = "ryanlintag@gmail.com";
            var activateRequest = new ActivateUserRequestCommand(userId, updatedBy);
            var result = await sender.Send(activateRequest);
            if (result.IsSuccess)
            {
                return Results.Ok();
            }
            else
            {
                return result.ToProblemDetails();
            }
        }
        public async Task<IResult> DeactivateUser(HttpContext context, ISender sender, [FromRoute] Guid userId)
        {
            var updatedBy = "ryanlintag@gmail.com";
            var deactivateRequest = new DeactivateUserRequestCommand(userId, updatedBy);
            var result = await sender.Send(deactivateRequest);
            if (result.IsSuccess)
            {
                return Results.Ok();
            }
            else
            {
                return result.ToProblemDetails();
            }
        }
    }
}
