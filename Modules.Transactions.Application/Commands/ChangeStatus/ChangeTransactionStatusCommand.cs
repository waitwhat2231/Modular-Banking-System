using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Modules.Transactions.Domain.Enums;
using System.Text.Json.Serialization;

namespace Modules.Transactions.Application.Commands.ChangeStatus
{
    public class ChangeTransactionStatusCommand : IRequest
    {
        [JsonIgnore]
        [BindNever]
        public int TransactionId { get; set; }
        public EnumTransactionStatus NewStatus { get; set; }
    }
}
