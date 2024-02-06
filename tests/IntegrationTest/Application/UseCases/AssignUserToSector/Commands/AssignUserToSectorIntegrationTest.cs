using Application.UseCases.Sector.Commands.AssignUserToSector.Commands;
using Application.UseCases.User.Queries;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Sector.Commands;
using IntegrationTest.Application.UseCases.User.Commands;

namespace IntegrationTest.Application.UseCases.AssignUserToSector.Commands;

[TestClass]
public class AssignUserToSectorIntegrationTest : Testing
{
    [TestMethod]
    public async Task AssignUserToSector()
    {
        var createUserIntegrationTest = new CreateUserIntegrationTest();
        _ = createUserIntegrationTest.CreateUser();

        var createSectorIntegrationTest = new CreateSectorIntegrationTest();
        _ = createSectorIntegrationTest.CreateSector();

        var command = AssignUserToSectorCommandFactory();

        var assigned = await SendAsync(command);
        Assert.IsTrue(assigned);

        var user = new GetUserByIdQuery { Id = CreateUserIntegrationTest.CreatedUser!.Id };

        var storedUser = await SendAsync(user);
        Assert.IsNotNull(storedUser);
        Assert.IsTrue(storedUser.Sectors!.Count > 0);
        Assert.AreEqual(CreateSectorIntegrationTest.CreatedSector!.Id, storedUser.Sectors[1].Id);
    }

    [DataTestMethod]
    public static AssignUserToSectorCommand AssignUserToSectorCommandFactory()
    {
        var command = new AssignUserToSectorCommand
        {
            UserId = CreateUserIntegrationTest.CreatedUser!.Id,
            SectorsId = [CreateSectorIntegrationTest.CreatedSector!.Id]
        };

        return command;
    }
}