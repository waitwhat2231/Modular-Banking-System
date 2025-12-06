using Modules.Transactions.Domain.Entities;

namespace Modules.Transactions.Domain.Handlers;
public abstract class TransactionHandler
{
    protected TransactionHandler _next;

    public TransactionHandler SetNext(TransactionHandler next)
    {
        _next = next;
        return next;
    }

    public abstract Task HandleAsync(Transaction transaction);
}
