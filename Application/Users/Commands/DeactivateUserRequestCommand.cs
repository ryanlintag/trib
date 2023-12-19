using Domain.Abstractions;
using Mediator;

namespace Application.Users.Commands
{
    public record DeactivateUserRequestCommand(
        Guid userId,
        string updatedBy) : ICommand<DomainResult>;
}

