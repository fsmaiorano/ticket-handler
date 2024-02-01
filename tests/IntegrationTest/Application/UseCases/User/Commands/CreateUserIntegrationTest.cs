using Application.UseCases.User.Commands.CreateUser;
using Application.UseCases.User.Queries;
using Bogus;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Holder.Commands;
using IntegrationTest.Application.UseCases.Sector.Commands;

namespace IntegrationTest.Application.UseCases.User.Commands;

[TestClass]
public class CreateUserIntegrationTest : Testing
{
    public static UserEntity? CreatedUser;

    [TestInitialize]
    public void TestInitialize()
    {
        CreatedUser = default;
    }

    [TestMethod]
    public async Task CreateUser()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var createSectorIntegrationTest = new CreateSectorIntegrationTest();
        _ = createSectorIntegrationTest.CreateSector();

        var command = CreateUserCommandFactory();

        var createdUserId = await SendAsync(command);
        Assert.IsNotNull(createdUserId);
        Assert.IsInstanceOfType(createdUserId, typeof(Guid));

        var query = new GetUserByEmailQuery
        {
            Email = command.Email
        };

        var user = await SendAsync(query);
        Assert.IsNotNull(user);
        Assert.AreEqual(command.Name, user?.Name);
        Assert.AreEqual(command.Email, user?.Email);
        Assert.AreEqual(command.Password, user?.Password);
        Assert.AreEqual(command.Username, user?.Username);

        CreatedUser = user;
    }

    [DataTestMethod]
    public static CreateUserCommand CreateUserCommandFactory()
    {
        var command = new CreateUserCommand
        {
            Name = new Faker().Name.FullName(),
            Email = new Faker().Internet.Email(),
            Password = new Faker().Internet.Password(),
            Username = new Faker().Internet.UserName(),
            HolderId = CreateHolderIntegrationTest.CreatedHolder!.Id,
            SectorsId = [CreateSectorIntegrationTest.CreatedSector!.Id]
        };

        return command;
    }
}
