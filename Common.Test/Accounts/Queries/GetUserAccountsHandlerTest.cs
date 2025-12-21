using FluentAssertions;
using Modules.Accounts.Application.Queries.GetUsersAccounts;
using Xunit;

namespace Common.Test.Accounts.Queries
{
    public class GetUserAccountsHandlerTest : TestBaseSetup
    {
        public GetUserAccountsHandlerTest() : base()
        {

        }
        [Fact]
        public async Task Handler_Should_Return_No_Accounts_When_No_Accounts_Beglong_To_User()
        {
            //Arrange
            var query = new GetUsersAccountsQuery("noExist");
            var queryHandler = new GetUsersAccountsQueryHandler(_userContextMock.Object, _mapperMock.Object, _accountRepositoryMock.Object);
            //Act
            var act = await queryHandler.Handle(query, default);
            //assert
            act.Should().BeEmpty();
        }
    }
}
