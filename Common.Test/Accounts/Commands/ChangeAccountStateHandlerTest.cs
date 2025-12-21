using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Enums;
using Common.SharedClasses.Exceptions;
using FluentAssertions;
using Modules.Accounts.Application.Command.ChangeState;
using Modules.Accounts.Domain.Entities;
using Moq;
using Xunit;

namespace Common.Test.Accounts.Commands;

public class ChangeAccountStateHandlerTest : TestBaseSetup
{
    public ChangeAccountStateHandlerTest() : base()
    {

    }
    [Fact]
    public async Task Handle_Should_Throw_Exception_When_account_Doesnt_Exist()
    {
        //Arrange
        var command = new ChangeAccountStateCommand()
        {
            AccountId = 15,
            NewState = AccountState.Suspended
        };
        var commmandHandler = new ChangeAccountStateCommandHandler(_accountRepositoryMock.Object, _mapperMock.Object);
        //Act
        Func<Task<AccountDto>> act = () => commmandHandler.Handle(command, default);
        //Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"account with id: {command.AccountId} doesn't exist");
    }


    [Fact]
    public async Task Handle_Should_Work_Given_Existing_Account_And_State()
    {
        //Arrange
        _accountRepositoryMock
        .Setup(x => x.AddAsync(It.IsAny<Account>()))
        .ReturnsAsync((Account account) => account);
        _mapperMock
          .Setup(x => x.Map<AccountDto>(It.IsAny<Account>()))
          .Returns(new AccountDto());

        var accounts = new List<Account>
{
new Account("user-id", AccountType.Checking)
{
    Id = 1
},
new Account("user-id", AccountType.Saving)
{
    Id = 2
}
};


        _accountRepositoryMock.Setup(arm => arm.FindByIdAsync(It.IsAny<int>()))
.ReturnsAsync((int id) => accounts.SingleOrDefault(a => a.Id == id));

        var command = new ChangeAccountStateCommand()
        {
            AccountId = 2,
            NewState = AccountState.Suspended
        };
        var commmandHandler = new ChangeAccountStateCommandHandler(_accountRepositoryMock.Object, _mapperMock.Object);
        //Act
        var act = await commmandHandler.Handle(command, default);
        //Assert
        act.Should().BeOfType<AccountDto>();
        accounts[1].State.Should().Be(AccountState.Suspended);
    }
}
