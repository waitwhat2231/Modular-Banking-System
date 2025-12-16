using Common.SharedClasses.Dtos.Transactions;
using MediatR;

namespace Modules.Transactions.Application.Queries
{
    public class GetTransactionRulesQuery : IRequest<List<TransactionRulesDto>>
    {
    }
}
