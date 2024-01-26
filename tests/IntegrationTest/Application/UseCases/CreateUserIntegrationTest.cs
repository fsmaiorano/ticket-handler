using Application.UseCases.User.Commands.CreateUser;
using Application.UseCases.User.Queries;
using Bogus;

namespace IntegrationTest.Application.UseCases;

[TestClass]
public class CreateUserIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task CreateUser()
    {
        var command = new CreateUserCommand
        {
            Name = new Faker().Name.FullName(),
            Email = new Faker().Internet.Email(),
            Password = new Faker().Internet.Password(),
            Username = new Faker().Internet.UserName()
        };

        var createdUserId = await SendAsync(command);

        var query = new GetUserByEmail
        {
            Email = command.Email
        };

        var user = await SendAsync(query);
        Assert.IsNotNull(user);
        Assert.AreEqual(command.Name, user?.Name);
        Assert.AreEqual(command.Email, user?.Email);
        Assert.AreEqual(command.Password, user?.Password);
        Assert.AreEqual(command.Username, user?.Username);
    }
}
