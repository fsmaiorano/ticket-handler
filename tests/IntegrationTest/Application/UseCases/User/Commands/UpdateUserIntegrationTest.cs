using Application.UseCases.User.Commands.UpdateUser;
using Application.UseCases.User.Queries;
using Bogus;

namespace IntegrationTest.Application.UseCases.User.Commands;

[TestClass]
public class UpdateUserIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
        var createUserIntegrationTest = new CreateUserIntegrationTest();
        _ = createUserIntegrationTest.CreateUser();
    }

    [TestMethod]
    public async Task UpdateUser()
    {
        var user = CreateUserIntegrationTest.CreatedUser;

        var command = new UpdateUserCommand
        {
            Id = user!.Id,
            Name = new Faker().Name.FullName(),
            Email = new Faker().Internet.Email(),
            Password = new Faker().Internet.Password(),
        };

        var updatedUserResponse = await SendAsync(command);

        Assert.IsNotNull(updatedUserResponse);
        Assert.IsTrue(updatedUserResponse.Success);

        var query = new GetUserByEmailQuery
        {
            Email = command.Email
        };

        var getUserByEmailResponse = await SendAsync(query);

        Assert.IsNotNull(getUserByEmailResponse);
        Assert.IsNotNull(getUserByEmailResponse.User);
        Assert.AreEqual(command.Name, getUserByEmailResponse.User.Name);
        Assert.AreEqual(command.Email, getUserByEmailResponse.User.Email);
        Assert.AreEqual(command.Password, getUserByEmailResponse.User.Password);
    }
}
