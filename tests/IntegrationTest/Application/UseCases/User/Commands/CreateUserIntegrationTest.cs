using Application.UseCases.User.Commands.CreateUser;
using Application.UseCases.User.Queries;
using Bogus;
using Domain.Constants;
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

        var createUserResponse = await SendAsync(command);
        Assert.IsNotNull(createUserResponse);
        Assert.IsNotNull(createUserResponse.User);
        Assert.IsTrue(createUserResponse.Success);

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

        CreatedUser = getUserByEmailResponse.User;
    }

    [DataTestMethod]
    public static CreateUserCommand CreateUserCommandFactory()
    {
        var command = new CreateUserCommand
        {
            Name = new Faker().Name.FullName(),
            Email = new Faker().Internet.Email(),
            Password = new Faker().Internet.Password(),
            HolderId = CreateHolderIntegrationTest.CreatedHolder!.Id,
            SectorsId = [CreateSectorIntegrationTest.CreatedSector!.Id],
            Role = UserRoles.Administrator
        };

        return command;
    }
}
