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

        var updatedUserId = await SendAsync(command);

        Assert.IsNotNull(updatedUserId);
        Assert.IsInstanceOfType(updatedUserId, typeof(Guid));
        Assert.AreEqual(updatedUserId, user.Id);

        var query = new GetUserByEmailQuery
        {
            Email = command.Email
        };

        var updatedUser = await SendAsync(query);

        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(command.Name, updatedUser?.Name);
        Assert.AreEqual(command.Email, updatedUser?.Email);
        Assert.AreEqual(command.Password, updatedUser?.Password);
    }
}
