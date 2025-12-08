using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Modules.Transactions.Application.Commands
{
    public class DepositCommand : IRequest
    {
        [JsonIgnore]
        [BindNever]
        public int AccountId { get; set; }
        public int Amount { get; set; }
    }
    public class WithdrawalCommand : IRequest
    {
        [JsonIgnore]
        [BindNever]
        public int AccountId { get; set; }
        public int Amount { get; set; }
    }
    public class TransferCommand : IRequest
    {
        [JsonIgnore]
        [BindNever]
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public int Amount { get; set; }
    }

}
