using Common.SharedClasses.Dtos.Transactions;
using Common.SharedClasses.Pagination;
using MediatR;

namespace Modules.Transactions.Application.Queries.GetAll
{
    public class GetTransactionsQuery(int pageNum, int pageSize, int? accountId, DateTime? from, DateTime? to) : IRequest<PagedEntity<TransactionDto>>
    {
        public int PageNum { get; set; } = pageNum;
        public int PageSize { get; set; } = pageSize;
        // public string? UserId { get; set; }
        public int? AccountId { get; set; } = accountId;
        public DateTime? From { get; set; } = from;
        public DateTime? To { get; set; } = to;
    }
}
