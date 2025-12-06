using Modules.Transactions.Domain.Enums;

namespace Modules.Transactions.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public EnumTransactionType TransactionType { get; set; }
        public int Amount { get; set; }
        public EnumTransactionStatus Status { get; set; }
        public string ApprovedByUserId { get; set; } = "System";
        public DateTime ApprovedAt { get; set; }
    }
}
