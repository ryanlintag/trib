namespace Domain.Abstractions
{
    public class TrackableEvent<T> where T : IDomainEvent
    {
        protected TrackableEvent() { }
        public TrackableEvent(EntityId id, Int64 version, T evt)
        {
            this.StreamId = id;
            this.Version = version;
            this.EventType = typeof(T).ToString();
            this.JsonData = evt.ToJsonString();
        }
        public EntityId StreamId { get; private set; }
        public Int64 Version { get; private set; }
        public string EventType { get; private set; }
        public string JsonData { get; private set; }
    }
}
