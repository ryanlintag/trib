using Application.Repositories;
using Domain.Abstractions;
using Domain.Users;
using Domain.ValueObjects;
using Mediator;

namespace Application.Users.Commands
{
    public sealed class UpdateUserRequestCommandHandler : ICommandHandler<UpdateUserRequestCommand, DomainResult>
    {
        private IUserRepository _userRepository { get; set; }

        public UpdateUserRequestCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async ValueTask<DomainResult> Handle(UpdateUserRequestCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var userId = new UserId(command.userId);
                var user = await _userRepository.GetUserById(userId);
                if (user == null)
                {
                    return DomainResult.Failure(new DomainError("User.UpdateUserRequestCommandHandler", "User id not found"));
                }
                var email = new Email(command.email);

                var userByEmail = await _userRepository.GetUserByEmail(email);
                if (userByEmail != null && userByEmail.Id != user.Id)
                {
                    return DomainResult.Failure(new DomainError("User.UpdateUserRequestCommandHandler", "Email is already used by another user"));
                }
                var name = new Name(command.lastName, command.firstName, command.middleName);
                var role = new Role(command.role);
                var updatedBy = new UpdatedByUser(new Email(command.updatedBy));
                user.UpdateDetails(email, name, role, command.isActive, updatedBy);

                await _userRepository.Update(user);
                await _userRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                return DomainResult.Failure(new DomainError("User.UpdateUserRequestCommandHandler", ex.Message));
            }

            return DomainResult.Success();
        }
    }
}
