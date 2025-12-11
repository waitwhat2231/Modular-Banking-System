using AutoMapper;
using Common.SharedClasses.Dtos.Transactions;
using Common.SharedClasses.Services;
using Modules.Transactions.Domain.Entities;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Services;

public class TransactionService(ITransactionsRepository transactionsRepository, IMapper mapper) : ITransactionService
{
    public async Task AddTransaction(AddTransactionDto addTransactionDto)
    {
        var trans = mapper.Map<Transaction>(addTransactionDto);
        await transactionsRepository.AddAsync(trans);
    }
    public async Task AddTransactionBatch(List<AddTransactionDto> transactionList)
    {
        var trans = mapper.Map<List<Transaction>>(transactionList);
        await transactionsRepository.AddBatch(trans);
    }

}
