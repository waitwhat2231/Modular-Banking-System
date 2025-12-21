using AutoMapper;
using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Dtos.Users;
using Common.SharedClasses.Enums;
using Common.SharedClasses.Exceptions;
using Common.SharedClasses.Services;
using FluentAssertions;
using Modules.Accounts.Application.Command.Create;
using Modules.Accounts.Domain.Entities;
using Modules.Accounts.Domain.Repositories;
using Moq;
using Xunit;

namespace Common.Test.Accounts;

public class CreateAccountHandlerTest
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly Mock<IMapper> _mapperMock;
    public CreateAccountHandlerTest()
    {
        _accountRepositoryMock = new();
        _userContextMock = new();
        _mapperMock = new();
    }
    [Fact]
    public async Task Handle_Should_Throw_Not_Found_Exception_When_Parent_Account_Does_Not_Exist()
    {
        //Arrange
        var currentUser = new CurrentUser("87e57f8e-809c-41f4-b827-69da1295ccf8", "newtest@test.com", ["User"]);
        _userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);

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

        var command = new CreateAccountCommand();
        // command.UserId = "87e57f8e-809c-41f4-b827-69da1295ccf8";
        command.ParentAccountId = -1;
        command.Type = AccountType.Checking;
        var commandHandler = new CreateAccountCommandHandler(_accountRepositoryMock.Object, _userContextMock.Object, _mapperMock.Object);


        //Act
        Func<Task<AccountDto>> act = () => commandHandler.Handle(command, default);


        //Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Parent account with id: {command.ParentAccountId} doesn't exist");

    }
    [Fact]
    public async Task Handle_Should_Work_Given_User_And_No_Parent_Account()
    {
        //Arrange
        var currentUser = new CurrentUser("87e57f8e-809c-41f4-b827-69da1295ccf8", "newtest@test.com", ["User"]);
        _userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);
        _accountRepositoryMock
        .Setup(x => x.AddAsync(It.IsAny<Account>()))
        .ReturnsAsync((Account account) => account);

        _mapperMock
            .Setup(x => x.Map<AccountDto>(It.IsAny<Account>()))
            .Returns(new AccountDto());
        var command = new CreateAccountCommand();
        // command.UserId = "87e57f8e-809c-41f4-b827-69da1295ccf8";
        command.ParentAccountId = null;
        command.Type = AccountType.Saving;
        var commandHandler = new CreateAccountCommandHandler(_accountRepositoryMock.Object, _userContextMock.Object, _mapperMock.Object);
        //Act
        AccountDto act = await commandHandler.Handle(command, default);


        //Assert
        act.Should().BeOfType<AccountDto>();
    }





    [Fact]
    public async Task Handle_Should_Add_Child_To_Existing_Parent()
    {
        // Arrange
        var currentUser = new CurrentUser("user-id", "test@test.com", ["User"]);
        _userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);

        var parent = new Account("user-id", AccountType.Checking) { Id = 1 };
        var accounts = new List<Account> { parent };

        _accountRepositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => accounts.SingleOrDefault(a => a.Id == id));

        _accountRepositoryMock
            .Setup(x => x.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        _mapperMock
            .Setup(x => x.Map<AccountDto>(It.IsAny<Account>()))
            .Returns(new AccountDto());

        var command = new CreateAccountCommand
        {
            ParentAccountId = 1,
            Type = AccountType.Saving
        };

        var handler = new CreateAccountCommandHandler(
            _accountRepositoryMock.Object,
            _userContextMock.Object,
            _mapperMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        parent.Children.Should().HaveCount(1);
    }

}
