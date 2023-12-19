namespace Domain.ValueObjects
{
    public record Role
    {
        public static readonly string Admin = "admin";
        public static readonly string User = "user";
        public string Name { get; private set; }
        public Role(string name)
        {
            if (!isValid(name))
            {
                throw new DomainException("Invalid role");
            }
            Name = name;
        }

        private bool isValid(string name)
        {
            return name.Equals(Admin) || name.Equals(User);
        }
    }
}
