namespace Domain.Abstractions
{
    public abstract class Entity<TId> where TId : EntityId
    {
        public TId Id { get; protected set; }
        public Entity(TId id)
        {
            Id = id;
        }
        #pragma warning disable CS8618
        protected Entity() { }
        #pragma warning restore CS8618
    }
}
