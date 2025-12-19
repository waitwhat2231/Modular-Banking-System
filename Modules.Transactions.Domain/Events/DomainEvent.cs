namespace Modules.Transactions.Domain.Events;

public abstract record DomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
