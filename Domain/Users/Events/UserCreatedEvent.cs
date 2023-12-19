using Domain.ValueObjects;

namespace Domain.Users.Events
{
    public record UserCreatedEvent : UserBaseDomainEvent
    {
        private UserId _userId { get; set; }
        private Email _email { get; set; }
        private Name _name { get; set; }
        private Role _role { get; set; }
        private UpdatedByUser _updatedByUser { get; set; }
        public UserCreatedEvent(Guid id, string email, string firstName, string lastName, string? middleName, string role, string lastUpdatedBy, DateTime updatedDate) : base(updatedDate)
        {
            this._userId = new UserId(id);
            this._email = new Email(email);
            this._name = new Name(firstName, lastName, middleName);
            this._role = new Role(role);
            this._updatedByUser = new UpdatedByUser(new Email(lastUpdatedBy));
            this.Id = id;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
            this.Role = role;
            this.LastUpdatedBy = lastUpdatedBy;
        }
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string? MiddleName { get; private set; }
        public string Role { get; private set; }
        public string LastUpdatedBy { get; private set; }

        public override void Apply(User user)
        {
            user = new User(this._userId, this._email, this._name, this._role, true, this._updatedByUser, this.UpdatedDate);
        }
    }
}
