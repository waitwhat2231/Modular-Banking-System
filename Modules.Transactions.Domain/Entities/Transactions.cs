using Modules.Transactions.Domain.Enums;

namespace Modules.Transactions.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
        public EnumTransactionType TransactionType { get; set; }


    }
}
