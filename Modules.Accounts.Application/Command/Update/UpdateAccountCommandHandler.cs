using AutoMapper;
using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Enums;
using MediatR;
using Modules.Accounts.Domain.Repositories;

namespace Modules.Accounts.Application.Command.Update;

public class UpdateAccountCommandHandler(IAccountRepository accountRepository,
    IMapper mapper) : IRequestHandler<UpdateAccountCommand, AccountDto>
{
    public async Task<AccountDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.FindByIdAsync(request.AccountId);

        if (account == null)
            throw new InvalidOperationException("Account not found");

        if (!string.IsNullOrWhiteSpace(request.UserId) && account.UserId != request.UserId)
        {
            if (account.State == AccountState.Closed)
                throw new InvalidOperationException("Cannot modify a closed account.");

            account.UserId = request.UserId;
        }

        if (request.ParentAccountId != account.ParentAccountId)
        {
            if (account.State == AccountState.Closed)
                throw new InvalidOperationException("Cannot move a closed account.");

            account.ParentAccountId = request.ParentAccountId;
        }

        if (request.Type != account.Type)
        {
            if (account.State == AccountState.Closed)
                throw new InvalidOperationException("Cannot change the type of a closed account.");

            switch (request.Type)
            {
                case AccountType.Checking:
                    account.Type = AccountType.Checking;
                    break;

                case AccountType.Saving:
                    account.Type = AccountType.Saving;
                    break;

                case AccountType.Loan:
                    if (account.Balance > 0)
                        throw new InvalidOperationException("Cannot convert to loan while balance is positive.");

                    account.Type = AccountType.Loan;
                    break;

                case AccountType.Investment:
                    if (account.ParentAccountId == null)
                        throw new InvalidOperationException("Investment accounts must have a parent.");

                    account.Type = AccountType.Investment;
                    break;

                default:
                    throw new InvalidOperationException("Invalid account type.");
            }
        }

        await accountRepository.SaveChangesAsync();

        return mapper.Map<AccountDto>(account);
    }
}
