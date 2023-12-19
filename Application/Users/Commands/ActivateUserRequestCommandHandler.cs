using Application.Repositories;
using Domain.Abstractions;
using Domain.Users;
using Domain.ValueObjects;
using Mediator;

namespace Application.Users.Commands
{
    internal class ActivateUserRequestCommandHandler : ICommandHandler<ActivateUserRequestCommand, DomainResult>
    {
        private IUserRepository _userRepository {  get; set; }

        public ActivateUserRequestCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async ValueTask<DomainResult> Handle(ActivateUserRequestCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var id = new UserId(command.userId);
                var user = await _userRepository.GetUserById(id); 
                if (user == null) { return DomainResult.Failure(new DomainError("Users.ActivateUserRequestCommandHandler", "User not found.")); }
                var updatedBy = new UpdatedByUser(new Email(command.updatedBy));
                user.Activate(updatedBy, DateTime.UtcNow);

                await this._userRepository.ActivateUser(user);
                await this._userRepository.SaveChanges();
            }
            catch (Exception ex) 
            {
                return DomainResult.Failure(new DomainError("Users.ActivateUserRequestCommandHandler", ex.Message));
            }
            return DomainResult.Success();
        }
    }
}
