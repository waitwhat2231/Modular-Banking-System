using Common.SharedClasses.Enums;

namespace Modules.Transactions.Domain.Events;

public record TransactionCompletedDomainEvent(EnumTransactionType Type, string UserId, int Amount) : DomainEvent;
