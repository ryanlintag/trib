using Domain.Abstractions;

namespace Domain.Users
{
    public record UserId(Guid id) : EntityId(id);
}
