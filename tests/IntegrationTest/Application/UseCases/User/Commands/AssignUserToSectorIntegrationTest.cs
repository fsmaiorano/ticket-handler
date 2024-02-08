using Application.UseCases.User.Commands.AssignUserToSector;
using Application.UseCases.User.Queries;
using IntegrationTest.Application.UseCases.Sector.Commands;

namespace IntegrationTest.Application.UseCases.User.Commands;

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

        var assignUserToSectorResponse = await SendAsync(command);
        Assert.IsTrue(assignUserToSectorResponse.Success);

        var user = new GetUserByIdQuery { Id = CreateUserIntegrationTest.CreatedUser!.Id };

        var getUserByIdResponse = await SendAsync(user);
        Assert.IsNotNull(getUserByIdResponse);
        Assert.IsNotNull(getUserByIdResponse.User);
        Assert.IsNotNull(getUserByIdResponse.User.Sectors);
        Assert.IsTrue(getUserByIdResponse.User.Sectors!.Count > 0);
        Assert.AreEqual(CreateSectorIntegrationTest.CreatedSector!.Id, getUserByIdResponse.User.Sectors[1].Id);
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