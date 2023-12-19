using Domain.Abstractions;
using Domain.ValueObjects;

namespace Domain.Users.Events
{
    public record UserActivatedEvent : UserBaseDomainEvent
    {
        private UpdatedByUser _updatedByUser { get; set; }
        public UserActivatedEvent(string activatedBy, DateTime dateUpdated) : base(dateUpdated)
        {
            this._updatedByUser = new UpdatedByUser(new Email(activatedBy));
            this.ActivatedBy = activatedBy;
        }
        //This is only added here for readability purposes.
        public const bool IsActivated = true;
        public string ActivatedBy { get; private set; }

        public override void Apply(User user)
        {
            if(user == null) { throw new DomainException("Null user passed to UserActivatedEvent"); }
            user.Activate(this._updatedByUser, this.UpdatedDate);
        }
    }
}
