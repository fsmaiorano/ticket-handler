using Application.UseCases.User.Commands.DeleteUser;
using Application.UseCases.User.Queries;

namespace IntegrationTest.Application.UseCases.User.Commands;

[TestClass]
public class DeleteUserIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
        var createUserIntegrationTest = new CreateUserIntegrationTest();
        _ = createUserIntegrationTest.CreateUser();
    }

    [TestMethod]
    public async Task DeleteUser()
    {
        var user = CreateUserIntegrationTest.CreatedUser;

        var command = new DeleteUserCommand
        {
            Id = user!.Id
        };

        var deletedUserId = await SendAsync(command);
        Assert.IsNotNull(deletedUserId);
        Assert.IsInstanceOfType(deletedUserId, typeof(Guid));

        var query = new GetUserByIdQuery
        {
            Id = user.Id
        };

        var deletedUser = await SendAsync(query);
        Assert.IsNull(deletedUser);
    }
}
