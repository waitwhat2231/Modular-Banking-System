namespace Modules.Transactions.Domain.Enums
{
    public enum EnumTransactionType
    {
        Loan,
        Routine,

    }
    public enum EnumTransactionStatus
    {
        PendingManager,
        PendingAdmin,
        Approved,
        Rejected
    }
}
