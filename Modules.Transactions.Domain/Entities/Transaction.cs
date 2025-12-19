using Common.SharedClasses.Enums;
using Modules.Transactions.Domain.Events;

namespace Modules.Transactions.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int? FromAccountId { get; set; }
        public int? ToAccountId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public EnumTransactionType TransactionType { get; set; }
        public int Amount { get; set; }
        public EnumTransactionStatus Status { get; set; }
        public string? ApprovedByUserId { get; set; }
        public DateTime? ApprovedAt { get; set; }

        private readonly List<DomainEvent> domainEvents = [];

        public IReadOnlyCollection<DomainEvent> DomainEvents => domainEvents.AsReadOnly();

        public void Complete(string userId)
        {
            if (Status != EnumTransactionStatus.Approved)
                return;

            domainEvents.Add(new TransactionCompletedDomainEvent(
                Id,
                userId,
                Amount
            ));
        }

        public void ClearDomainEvents()
        => domainEvents.Clear();
    }
}
