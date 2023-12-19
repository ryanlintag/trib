using Application.Repositories;
using Domain.Abstractions;
using Domain.Users;
using Domain.ValueObjects;
using Mediator;

namespace Application.Users.Commands
{
    public sealed class DeactivateUserRequestCommandHandler : ICommandHandler<DeactivateUserRequestCommand, DomainResult>
    {
        private IUserRepository _userRepository { get; set; }

        public DeactivateUserRequestCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async ValueTask<DomainResult> Handle(DeactivateUserRequestCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var id = new UserId(command.userId);
                var user = await _userRepository.GetUserById(id);
                if (user == null) { return DomainResult.Failure(new DomainError("Users.DeactivateUserRequestCommandHandler", "User not found.")); }
                var updatedBy = new UpdatedByUser(new Email(command.updatedBy));
                user.Deactivate(updatedBy, DateTime.UtcNow);

                await this._userRepository.DeactivateUser(user);
                await this._userRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                return DomainResult.Failure(new DomainError("Users.DeactivateUserRequestCommandHandler", ex.Message));
            }
            return DomainResult.Success();
        }
    }
}
