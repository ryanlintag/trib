namespace Domain.Abstractions
{
    public interface IDomainEvent
    {
        string ToJsonString();
    }
}
