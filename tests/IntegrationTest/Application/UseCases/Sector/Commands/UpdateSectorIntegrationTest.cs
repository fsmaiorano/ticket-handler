using Application.UseCases.Sector.Commands.UpdateSector;
using Bogus;

namespace IntegrationTest.Application.UseCases.Sector.Commands;

[TestClass]
public class UpdateSectorIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
        var createSectorIntegrationTest = new CreateSectorIntegrationTest();
        _ = createSectorIntegrationTest.CreateSector();
    }

    [TestMethod]
    public async Task UpdateSector()
    {
        var sector = CreateSectorIntegrationTest.CreatedSector;

        var command = new UpdateSectorCommand
        {
            Id = sector!.Id,
            HolderId = sector!.HolderId,
            Name = new Faker().Name.FullName()
        };

        var updatedSectorResponse = await SendAsync(command);

        Assert.IsNotNull(updatedSectorResponse);
        Assert.IsTrue(updatedSectorResponse.Success);
    }
}