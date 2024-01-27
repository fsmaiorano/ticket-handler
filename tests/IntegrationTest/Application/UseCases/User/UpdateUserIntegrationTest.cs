using Application.UseCases.User.Commands.UpdateUser;
using Application.UseCases.User.Queries;
using Bogus;

namespace IntegrationTest.Application.UseCases.User;

[TestClass]
public class UpdateUserIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task UpdateUser()
    {
        var user = await CreateUserIntegrationTest.CreateUser();
        Assert.IsNotNull(user);
        
        var command = new UpdateUserCommand
        {
            Id = user!.Id,
            Name = new Faker().Name.FullName(),
            Email = new Faker().Internet.Email(),
            Password = new Faker().Internet.Password(),
            Username = new Faker().Internet.UserName()
        };

        var updatedUserId = await SendAsync(command);

        Assert.IsNotNull(updatedUserId);
        Assert.IsInstanceOfType(updatedUserId, typeof(Guid));
        Assert.AreEqual(updatedUserId, user.Id);

         var query = new GetUserByEmail
        {
            Email = command.Email
        };

        var updatedUser = await SendAsync(query);

        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(command.Name, updatedUser?.Name);
        Assert.AreEqual(command.Email, updatedUser?.Email);
        Assert.AreEqual(command.Password, updatedUser?.Password);
        Assert.AreEqual(command.Username, updatedUser?.Username);
    }
}
