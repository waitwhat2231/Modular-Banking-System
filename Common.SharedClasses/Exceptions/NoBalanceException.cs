namespace Common.SharedClasses.Exceptions
{
    public class NoBalanceException(int accountId) : Exception($"Account with Id {accountId} does not have enough balance")
    {
    }
}
