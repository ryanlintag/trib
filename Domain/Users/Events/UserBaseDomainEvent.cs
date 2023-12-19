using Domain.Abstractions;
using Newtonsoft.Json;

namespace Domain.Users.Events
{
    public abstract record UserBaseDomainEvent(DateTime UpdatedDate) : IDomainEvent
    {
        public virtual string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public abstract void Apply(User user);
    }
}
