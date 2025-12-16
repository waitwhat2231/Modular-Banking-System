using Common.SharedClasses.Dtos.Transactions;
using MediatR;

namespace Modules.Transactions.Application.Queries
{
    public class GetTransactionsQuery(int pageNum, int pageSize) : IRequest<List<TransactionDto>>
    {
        public int PageNum { get; set; } = pageNum;
        public int PageSize { get; set; } = pageSize;
    }
}
