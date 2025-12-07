using AutoMapper;
using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Exceptions;
using MediatR;
using Modules.Accounts.Domain.Entities;
using Modules.Accounts.Domain.Repositories;

namespace Modules.Accounts.Application.Command.Create;

public class CreateAccountCommandHandler(IAccountRepository accountRepository,
    IMapper mapper) : IRequestHandler<CreateAccountCommand, AccountDto>
{
    public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        Account newAccount;
        AccountDto result;

        if (request.ParentAccountId.HasValue)
        {
            newAccount = new Account(request.UserId, request.Type, request.ParentAccountId);
            var parent = await accountRepository.FindByIdAsync(request.ParentAccountId.Value);

            if (parent == null) throw new NotFoundException("Parent account not found", request.ParentAccountId.Value.ToString());

            parent.AddChild(newAccount);
            await accountRepository.SaveChangesAsync();
            result = mapper.Map<AccountDto>(parent);
            return result;
        }

        newAccount = new Account(request.UserId, request.Type);
        var created = await accountRepository.AddAsync(newAccount);

        result = mapper.Map<AccountDto>(created);
        return result;
    }
}
