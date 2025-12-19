using MediatR;
using Modules.Transactions.Application.Commands.CompleteTransaction;
using Modules.Transactions.Domain.Events;

namespace Modules.Transactions.Application.Events;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchAsync(IEnumerable<DomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            if (domainEvent is TransactionCompletedDomainEvent e)
            {
                await _mediator.Publish(new CompleteTransactionCommand(
                    e.UserId,
                    e.TransactionId
                ));
            }
        }
    }
}
