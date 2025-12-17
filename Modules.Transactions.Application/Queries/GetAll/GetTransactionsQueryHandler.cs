using AutoMapper;
using Common.SharedClasses.Dtos.Transactions;
using Common.SharedClasses.Enums;
using Common.SharedClasses.Pagination;
using Common.SharedClasses.Services;
using MediatR;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Queries.GetAll
{
    class GetTransactionsQueryHandler(ITransactionsRepository transactionsRepository, IUserContext userContext, IAccountService accountService,
        IMapper mapper) : IRequestHandler<GetTransactionsQuery, PagedEntity<TransactionDto>>
    {
        public async Task<PagedEntity<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            List<int> accountIds = [];
            if (request.AccountId == null && currentUser.Roles.First().Equals(nameof(EnumRoleNames.User)))
            {
                var accounts = await accountService.GetAccountsForUser(currentUser.Id);
                accountIds = [.. accounts.Select(a => a.Id)];
            }
            else if (request.AccountId != null)
            {
                accountIds.Add((int)request.AccountId);
            }

            var transactions = await transactionsRepository.GetTransactionsPaged(request.PageNum, request.PageSize, accountIds, request.From, request.To);
            var res = new PagedEntity<TransactionDto>()
            {
                Items = mapper.Map<List<TransactionDto>>(transactions.Items),
                TotalItems = transactions.TotalItems,
                PageNumber = request.PageNum,
                PageSize = request.PageSize,
            };
            return res;
        }
    }
}
