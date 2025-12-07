using Modules.Transactions.Domain.Entities;

namespace Modules.Transactions.Application.Handlers;

public abstract class TransactionHandler
{
    protected TransactionHandler _next;
    private readonly I

    public TransactionHandler SetNext(TransactionHandler next)
    {
        _next = next;
        return next;
    }

    public abstract Task HandleAsync(Transaction transaction);
}
