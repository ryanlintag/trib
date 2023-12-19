using Application.Repositories;
using Domain.Abstractions;
using Domain.Users;
using Domain.ValueObjects;
using Mediator;

namespace Application.Users.Commands
{
    public class CreateUserRequestCommandHandler : ICommandHandler<CreateUserRequestCommand, DomainResult>
    {
        private IUserRepository _repository;
        public CreateUserRequestCommandHandler(IUserRepository repository)
        {
            this._repository = repository;
        }
        async ValueTask<DomainResult> ICommandHandler<CreateUserRequestCommand, DomainResult>.Handle(CreateUserRequestCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var userId = new UserId(Guid.NewGuid());
                var name = new Name(command.firstName, command.lastName, command.middleName);
                var email = new Email(command.email);
                var role = new Role(command.role);
                var updateByUser = new UpdatedByUser(new Email(command.updatedBy));
                var updatedDate = DateTime.UtcNow;
                var user = new User(userId, email, name, role, true, updateByUser, updatedDate);
                if (_repository.UserExists(email.Address))
                {
                    throw new InvalidOperationException("User email already exists in the records");
                }
                await _repository.CreateNewUser(user);
                await _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                return DomainResult.Failure(new DomainError("Users.CreateUserRequestCommandHandler", ex.Message));
            }
            return DomainResult.Success();
        }
    }
}
