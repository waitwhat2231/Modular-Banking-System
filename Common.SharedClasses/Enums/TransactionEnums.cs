namespace Common.SharedClasses.Enums
{
    public enum EnumTransactionType
    {
        Loan,
        Routine,
        Deposit,
        Withdrawal,
        Transfer

    }
    public enum EnumTransactionStatus
    {
        PendingManager,
        PendingAdmin,
        Approved,
        Rejected
    }
}
