namespace Domain.ValueObjects
{
    public record Name
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public string? MiddleName { get; private set; }
        public string FullName 
        { 
            get 
            {
                return string.IsNullOrEmpty(this.MiddleName) ? 
                        $"{this.FirstName} {this.LastName}" : 
                        $"{this.FirstName} {this.MiddleName} {this.LastName}";
            } 
        }
        public static explicit operator string(Name name) => name.FullName;
        public Name(string lastName, string firstName, string? middleName)
        {
            if(string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName))
            {
                throw new DomainException("Name should contain first name and last name");
            }
            LastName = lastName;
            FirstName = firstName;
            if (!string.IsNullOrEmpty(middleName))
            {
                MiddleName = middleName;
            }
        }
    }
}
