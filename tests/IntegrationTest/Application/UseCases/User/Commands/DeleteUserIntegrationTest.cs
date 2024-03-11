using Application.UseCases.User.Commands.DeleteUser;

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

        var deletedUserResponse = await SendAsync(command);
        Assert.IsNotNull(deletedUserResponse);
        Assert.IsTrue(deletedUserResponse.Success);
    }
}
