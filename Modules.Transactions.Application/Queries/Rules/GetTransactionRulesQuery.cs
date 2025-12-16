using Common.SharedClasses.Dtos.Transactions;
using MediatR;

namespace Modules.Transactions.Application.Queries.Rules
{
    public class GetTransactionRulesQuery : IRequest<List<TransactionRulesDto>>
    {
    }
}
