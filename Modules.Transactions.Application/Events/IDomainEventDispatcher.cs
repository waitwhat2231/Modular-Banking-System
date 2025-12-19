using Modules.Transactions.Domain.Events;

namespace Modules.Transactions.Application.Events;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<DomainEvent> domainEvents);
}
