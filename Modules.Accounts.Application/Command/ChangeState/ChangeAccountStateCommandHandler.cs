using AutoMapper;
using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Enums;
using MediatR;
using Modules.Accounts.Domain.Repositories;

namespace Modules.Accounts.Application.Command.ChangeState;

public class ChangeAccountStateCommandHandler(IAccountRepository accountRepository,
    IMapper mapper) : IRequestHandler<ChangeAccountStateCommand, AccountDto>
{
    public async Task<AccountDto> Handle(ChangeAccountStateCommand request, CancellationToken cancellationToken)
    {
        AccountDto result;
        var existingAccount = await accountRepository.FindByIdAsync(request.AccountId);
        if (existingAccount == null) throw new InvalidOperationException("Account is not found");

        if (existingAccount.State == request.NewState)
        {
            result = mapper.Map<AccountDto>(existingAccount);
            return result;
        }

        switch (request.NewState)
        {
            case AccountState.Active:
                existingAccount.Activate();
                break;

            case AccountState.Frozen:
                existingAccount.Freeze();
                break;

            case AccountState.Suspended:
                existingAccount.Suspend();
                break;

            case AccountState.Closed:
                existingAccount.Close();
                break;

            default:
                throw new InvalidOperationException("Unknown account state");
        }

        await accountRepository.SaveChangesAsync();

        result = mapper.Map<AccountDto>(existingAccount);
        return result;
    }
}
