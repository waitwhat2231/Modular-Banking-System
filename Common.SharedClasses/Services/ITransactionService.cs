using Common.SharedClasses.Dtos.Transactions;

namespace Common.SharedClasses.Services
{
    public interface ITransactionService
    {
        public Task AddTransaction(TransactionDto addTransactionDto);
        Task AddTransactionBatch(List<TransactionDto> transactionList);
    }
}
