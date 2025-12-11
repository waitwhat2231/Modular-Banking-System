using Common.SharedClasses.Enums;

namespace Common.SharedClasses.Dtos.Transactions
{
    public class AddTransactionDto
    {
        public int Amount { get; set; }
        public EnumTransactionType Type { get; set; }
        public EnumTransactionStatus Status { get; set; }
        public int? FromAccountId { get; set; }
        public int? ToAccountId { get; set; }
        public string? ApprovedByUserId { get; set; }

        public DateTime? ApprovedAt { get; set; }
    }
}
