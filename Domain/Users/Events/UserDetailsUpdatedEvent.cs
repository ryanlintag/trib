using Domain.ValueObjects;
using Newtonsoft.Json;
namespace Domain.Users.Events
{
    public record UserDetailsUpdatedEvent : UserBaseDomainEvent
    {
        private Email _email { get; init; }
        private Name _name { get; init; }
        private Role _role { get; init; }
        private UpdatedByUser _updatedBy { get; init; }

        public User ToUser(UserId userId)
        {
            return new User(userId, this._email, this._name, this._role, this.IsActive, this._updatedBy, this.UpdatedDate);
        }

        public override void Apply(User user)
        {
            if (user == null) { throw new DomainException("Null user passed to UserDetailsUpdatedEvent"); }
            var userId = user.Id;
            user = this.ToUser(userId);
        }

        public UserDetailsUpdatedEvent(string email, string firstName, string lastName, string? middleName, string role, bool isActive, string updatedBy, DateTime dateUpdated) : base(dateUpdated)
        {
            this._email = new Email(email);
            this._name = new Name(lastName, firstName, middleName);
            this._role = new Role(role);
            this._updatedBy = new UpdatedByUser(new Email(email));
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
            this.Role = role;
            this.IsActive = isActive;
            this.UpdatedBy = updatedBy;
        }
        public string Email { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string? MiddleName { get; init; }
        public string Role { get; init; }
        public bool IsActive { get; init; }
        public string UpdatedBy  { get; init; }
    }
}
