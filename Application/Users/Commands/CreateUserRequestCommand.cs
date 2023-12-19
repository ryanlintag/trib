using Domain.Abstractions;
using Mediator;

namespace Application.Users.Commands
{
    public record class CreateUserRequestCommand(string email, string firstName, string middleName, string lastName, string role, string updatedBy) : ICommand<DomainResult>;
}
