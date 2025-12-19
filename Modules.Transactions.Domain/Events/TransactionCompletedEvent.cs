namespace Modules.Transactions.Domain.Events;

public record TransactionCompletedDomainEvent(int TransactionId, string UserId, int Amount) : DomainEvent;
