using Common.SharedClasses.Dtos.Transactions;

namespace Common.SharedClasses.Services
{
    public interface ITransactionService
    {
        public Task AddTransaction(AddTransactionDto addTransactionDto);
        Task AddTransactionBatch(List<AddTransactionDto> transactionList);
    }
}
