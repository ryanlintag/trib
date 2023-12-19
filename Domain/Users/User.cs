using Domain.Abstractions;
using Domain.ValueObjects;

namespace Domain.Users
{
    public class User : AggregateRoot<UserId>
    {
        public User(UserId userId, Email email, Name name, Role role, bool isActive, UpdatedByUser updatedByUser, DateTime dateLastUpdated)
        {
            this.Id = userId;
            this.Email = email;
            this.Name = name;
            this.Role = role;
            this.IsActive = isActive;
            this.UpdatedByUser = updatedByUser;
            this.DateLastUpdated = dateLastUpdated;
        }
        public Email Email { get; private set; }
        public Name Name { get; private set; }
        public Role Role { get; private set; }
        public bool IsActive { get; private set; }
        public UpdatedByUser UpdatedByUser { get; private set; }
        public DateTime DateLastUpdated { get; private set; }
#pragma warning disable CS8618
        private User() { }
#pragma warning restore CS8618

        public void Activate(UpdatedByUser updatedByUser, DateTime activatedDate)
        {
            this.IsActive = true;
            this.DateLastUpdated = activatedDate;
            this.UpdatedByUser = updatedByUser;
        }
        public void Deactivate(UpdatedByUser updatedByUser, DateTime deactivatedDate)
        {
            this.IsActive = false;
            this.DateLastUpdated = deactivatedDate;
            this.UpdatedByUser = updatedByUser;
        }

        public void UpdateDetails(Email email, Name name, Role role, bool isActive, UpdatedByUser updatedBy)
        {
            this.Email = email;
            this.Name = name;
            this.Role = role;
            this.IsActive = isActive;
            this.UpdatedByUser = updatedBy;
            this.DateLastUpdated = DateTime.UtcNow;
        }
    }
}
