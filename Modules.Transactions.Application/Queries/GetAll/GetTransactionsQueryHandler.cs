using AutoMapper;
using Common.SharedClasses.Dtos.Transactions;
using Common.SharedClasses.Enums;
using Common.SharedClasses.Pagination;
using Common.SharedClasses.Services;
using MediatR;
using Modules.Transactions.Domain.Repositories;

namespace Modules.Transactions.Application.Queries.GetAll
{
    class GetTransactionsQueryHandler(ITransactionsRepository transactionsRepository, IUserContext userContext, IUsersService userService, IAccountService accountService,
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

            var transactions = await transactionsRepository.GetTransactionsPaged(request.PageNum, request.PageSize, accountIds, request.From, request.To, request.Type, request.Status);
            var userIds = transactions.Items.Where(t => t.ApprovedByUserId != null).Select(t => t.ApprovedByUserId).ToList();
            var users = await userService.GetUsersFromIds(userIds);
            var userLookups = users.ToDictionary(u => u.Id, u => u.UserName);
            var res = new PagedEntity<TransactionDto>()
            {
                Items = mapper.Map<List<TransactionDto>>(transactions.Items),
                TotalItems = transactions.TotalItems,
                PageNumber = request.PageNum,
                PageSize = request.PageSize,
            };
            foreach (var item in res.Items)
            {
                if (item.ApprovedByUserId != null)
                {
                    item.ApprovedByUserName = userLookups[item.ApprovedByUserId];
                }
            }
            return res;
        }
    }
}
