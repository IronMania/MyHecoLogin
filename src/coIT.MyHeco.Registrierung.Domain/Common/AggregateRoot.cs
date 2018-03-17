using System.Collections.Generic;

namespace coIT.MyHeco.Registrierung.Domain.Common
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(IDomainEvent newEvent)
        {
            _domainEvents.Add(newEvent);
        }

        public void ClearEvents()
        {
            _domainEvents.Clear();
        }
    }
}