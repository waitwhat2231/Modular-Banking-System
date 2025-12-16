using AutoMapper;
using Common.SharedClasses.Dtos.Transactions;
using MediatR;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Queries
{
    class GetTransactionsQueryHandler(ITransactionsRepository transactionsRepository, IMapper mapper) : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
    {
        public async Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await transactionsRepository.GetTrnasctionsPaged(request.PageNum, request.PageSize);
            var res = mapper.Map<List<TransactionDto>>(transactions);
            return res;
        }
    }
}
