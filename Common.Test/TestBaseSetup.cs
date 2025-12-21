using AutoMapper;
using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Dtos.Users;
using Common.SharedClasses.Enums;
using Common.SharedClasses.Services;
using Modules.Accounts.Domain.Entities;
using Modules.Accounts.Domain.Repositories;
using Moq;

namespace Common.Test;

public class TestBaseSetup
{
    protected readonly Mock<IAccountRepository> _accountRepositoryMock;
    protected readonly Mock<IUserContext> _userContextMock;
    protected readonly Mock<IMapper> _mapperMock;
    public List<Account> accounts;
    public TestBaseSetup()
    {
        _accountRepositoryMock = new();
        _userContextMock = new();
        _mapperMock = new();
        accounts = new List<Account>() { new Account("user1", AccountType.Saving)
    {
        Id = 1 ,
        Balance = 0
    },
   new Account("User2", AccountType.Checking)
   {
       Id = 2,
       Balance = 0
   }
    };




        _accountRepositoryMock
.Setup(x => x.AddAsync(It.IsAny<Account>()))
.Callback<Account>(a => accounts.Add(a))
.ReturnsAsync((Account a) => a);



        var currentUser = new CurrentUser("87e57f8e-809c-41f4-b827-69da1295ccf8", "newtest@test.com", ["User"]);
        _userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);


        _mapperMock
            .Setup(x => x.Map<AccountDto>(It.IsAny<Account>()))
            .Returns((Account acc) => new AccountDto()
            {
                Balance = acc.Balance,
                UserId = acc.UserId,
                ParentAccountId = acc.ParentAccountId,
                Id = acc.Id,
                State = acc.State,
                Type = acc.Type
            });

        _mapperMock
            .Setup(x => x.Map<List<AccountDto>>(It.IsAny<List<Account>>()))
            .Returns((List<Account> accs) => accs.Select(acc => new AccountDto()
            {
                Balance = acc.Balance,
                UserId = acc.UserId,
                ParentAccountId = acc.ParentAccountId,
                Id = acc.Id,
                State = acc.State,
                Type = acc.Type
            }
                ).ToList());


        _accountRepositoryMock.Setup(arm => arm.FindByIdAsync(It.IsAny<int>()))
    .ReturnsAsync((int id) => accounts.SingleOrDefault(a => a.Id == id));


        _accountRepositoryMock
    .Setup(x => x.SaveChangesAsync())
    .Returns(Task.CompletedTask);


        _accountRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<string>()))
          .ReturnsAsync((string id) => accounts.Where(a => a.UserId == id).ToList());
    }
}
