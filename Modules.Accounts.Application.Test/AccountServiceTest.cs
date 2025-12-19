using AutoMapper;
using Common.SharedClasses.Dtos.Users;
using Common.SharedClasses.Exceptions;
using Common.SharedClasses.Services;
using FluentAssertions;
using Modules.Accounts.Application.Command.Create;
using Modules.Accounts.Domain.Repositories;
using Moq;
using Xunit;

namespace Modules.Accounts.Application.Test;

public class AccountServiceTest
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly Mock<IMapper> _mapperMock;
    public AccountServiceTest()
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
        var command = new CreateAccountCommand();
        // command.UserId = "87e57f8e-809c-41f4-b827-69da1295ccf8";
        command.ParentAccountId = -1;
        command.Type = Common.SharedClasses.Enums.AccountType.Checking;
        var commandHandler = new CreateAccountCommandHandler(_accountRepositoryMock.Object, _userContextMock.Object, _mapperMock.Object);


        //Act
        Func<Task<Common.SharedClasses.Dtos.Accounts.AccountDto>> act = () => commandHandler.Handle(command, default);


        //Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Parent account with id: {command.ParentAccountId} doesn't exist");

    }


}
