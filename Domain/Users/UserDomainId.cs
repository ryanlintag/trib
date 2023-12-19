using Domain.Abstractions;

namespace Domain.Users
{
    public record UserDomainId(Guid id) : EntityId(id);
}
