using Common.SharedClasses.Dtos.Transactions;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Modules.Transactions.Application.Commands
{
    public class DepositCommand : IRequest<TransactionDto>
    {
        [JsonIgnore]
        [BindNever]
        public int AccountId { get; set; }
        public int Amount { get; set; }
    }
    public class WithdrawalCommand : IRequest<TransactionDto>
    {
        [JsonIgnore]
        [BindNever]
        public int AccountId { get; set; }
        public int Amount { get; set; }
    }
    public class TransferCommand : IRequest<TransactionDto>
    {
        [JsonIgnore]
        [BindNever]
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public int Amount { get; set; }
    }

}
