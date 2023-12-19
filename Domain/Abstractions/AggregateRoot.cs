namespace Domain.Abstractions
{
    public abstract class AggregateRoot<TId> : Entity<TId> where TId : EntityId
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public List<IDomainEvent> DomainEvents { get { return _domainEvents; } }

        protected void Raise(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
