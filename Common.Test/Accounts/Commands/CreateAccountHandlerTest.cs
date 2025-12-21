using Common.SharedClasses.Dtos.Accounts;
using Common.SharedClasses.Enums;
using Common.SharedClasses.Exceptions;
using FluentAssertions;
using Modules.Accounts.Application.Command.Create;
using Modules.Accounts.Domain.Entities;
using Xunit;

namespace Common.Test.Accounts.Commands;

public class CreateAccountHandlerTest : TestBaseSetup
{
    public CreateAccountHandlerTest() : base()
    {

    }
    [Fact]
    public async Task Handle_Should_Throw_Not_Found_Exception_When_Parent_Account_Does_Not_Exist()
    {
        //Arrange
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
        var command = new CreateAccountCommand();
        // command.UserId = "87e57f8e-809c-41f4-b827-69da1295ccf8";
        command.ParentAccountId = null;
        command.Type = AccountType.Saving;
        var commandHandler = new CreateAccountCommandHandler(_accountRepositoryMock.Object, _userContextMock.Object, _mapperMock.Object);
        //Act
        AccountDto act = await commandHandler.Handle(command, default);


        //Assert
        act.Should().BeOfType<AccountDto>();
        accounts.Should().HaveCount(3);
    }





    [Fact]
    public async Task Handle_Should_Add_Child_To_Existing_Parent()
    {
        // Arrange
        var parent = new Account("87e57f8e-809c-41f4-b827-69da1295ccf8", AccountType.Checking) { Id = 3 };
        accounts.Add(parent);

        var command = new CreateAccountCommand
        {
            ParentAccountId = 3,
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
