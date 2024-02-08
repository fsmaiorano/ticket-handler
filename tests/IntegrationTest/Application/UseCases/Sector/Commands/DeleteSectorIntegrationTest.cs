using Application.UseCases.Sector.Queries;
using Application.UseCases.User.Commands.SectorUser;

namespace IntegrationTest.Application.UseCases.Sector.Commands;

[TestClass]
public class DeleteSectorIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
        var createUserIntegrationTest = new CreateSectorIntegrationTest();
        _ = createUserIntegrationTest.CreateSector();
    }


    [TestMethod]
    public async Task DeleteSector()
    {
        var sector = CreateSectorIntegrationTest.CreatedSector;

        var command = new DeleteSectorCommand
        {
            Id = sector!.Id
        };

        var deletedSectorResponse = await SendAsync(command);
        Assert.IsNotNull(deletedSectorResponse);
        Assert.IsTrue(deletedSectorResponse.Success);
    }
}
