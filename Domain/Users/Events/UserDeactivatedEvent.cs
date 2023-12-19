using Domain.ValueObjects;

namespace Domain.Users.Events
{
    public record UserDeactivatedEvent : UserBaseDomainEvent
    {
        private UpdatedByUser _updatedByUser { get; set; }
        public UserDeactivatedEvent(string deactivatedBy, DateTime dateUpdated) : base(dateUpdated)
        {
            this._updatedByUser = new UpdatedByUser(new Email(deactivatedBy));
            this.DeactivatedBy = deactivatedBy;
        }
        //This is only added here for readability purposes.
        public const bool IsActivated = false;
        public string DeactivatedBy { get; private set; }

        public override void Apply(User user)
        {
            if (user == null) { throw new DomainException("Null user passed to UserDeactivatedEvent"); }
            user.Deactivate(this._updatedByUser, this.UpdatedDate);
        }
    }
}
