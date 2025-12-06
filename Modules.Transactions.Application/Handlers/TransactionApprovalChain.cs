using Modules.Transactions.Domain.Entities;

namespace Modules.Transactions.Application.Handlers;

public class TransactionApprovalChain
{
    private readonly TransactionHandler _handler;

    public TransactionApprovalChain(TransactionHandler handler)
    {
        _handler = handler;
    }

    public Task ExecuteAsync(Transaction tx)
        => _handler.HandleAsync(tx);
}
