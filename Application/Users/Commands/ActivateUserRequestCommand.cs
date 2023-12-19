using Domain.Abstractions;
using Mediator;

namespace Application.Users.Commands
{
    public record ActivateUserRequestCommand(
        Guid userId,
        string updatedBy) : ICommand<DomainResult>;
}
