using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Modules.Accounts.Application.Command.Update;

public class UpdateAccountCommand : IRequest<AccountDto>
{
    [JsonIgnore]
    [BindNever]
    public int AccountId { get; set; }
    public int? ParentAccountId { get; set; }
    public AccountType Type { get; set; }
}
