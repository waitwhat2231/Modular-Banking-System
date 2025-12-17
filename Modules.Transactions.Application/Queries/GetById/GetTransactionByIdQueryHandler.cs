using AutoMapper;
using Common.SharedClasses.Dtos.Transactions;
using MediatR;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Queries.GetById
{
    class GetTransactionByIdQueryHandler(ITransactionsRepository transactionsRepository, IMapper mapper) : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
    {
        public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await transactionsRepository.FindByIdAsync(request.Id);
            var res = mapper.Map<TransactionDto>(transaction);
            return res;
        }
    }
}
