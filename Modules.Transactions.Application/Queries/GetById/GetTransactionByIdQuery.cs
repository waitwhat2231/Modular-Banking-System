using Common.SharedClasses.Dtos.Transactions;
using MediatR;

namespace Modules.Transactions.Application.Queries.GetById
{
    public class GetTransactionByIdQuery(int id) : IRequest<TransactionDto>
    {
        public int Id { get; set; } = id;
    }
}
