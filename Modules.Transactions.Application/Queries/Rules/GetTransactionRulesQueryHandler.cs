using AutoMapper;
using Common.SharedClasses.Dtos.Transactions;
using MediatR;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Queries.Rules
{
    public class GetTransactionRulesQueryHandler(ITransactionRulesRepository transactionRulesRepository, IMapper mapper) : IRequestHandler<GetTransactionRulesQuery, List<TransactionRulesDto>>
    {
        public async Task<List<TransactionRulesDto>> Handle(GetTransactionRulesQuery request, CancellationToken cancellationToken)
        {
            var rules = await transactionRulesRepository.GetAllAsync();
            var res = mapper.Map<List<TransactionRulesDto>>(rules);
            foreach (var rule in res)
            {
                rule.HandlerName = rule.HandlerName.Replace("ApprovalHandler", string.Empty);
            }
            return res;
        }
    }
}
