using Domain.Abstractions;
using Domain.Users.Events;

namespace Domain.Users
{
    public class UserDomainEvent : TrackableEvent<UserBaseDomainEvent>
    {
        private UserDomainEvent() { }
        public UserDomainEvent(UserDomainId id, UserId userId, long version, UserBaseDomainEvent evt) : base(userId, version, evt)
        {
            this.Id = id;
        }
        public UserDomainId Id { get; set; }

    }
}
