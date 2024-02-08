using Application.UseCases.Sector.Commands.UpdateSector;
using Application.UseCases.Sector.Queries;
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

        var updatedSectorId = await SendAsync(command);

        Assert.IsNotNull(updatedSectorId);
        Assert.IsInstanceOfType(updatedSectorId, typeof(Guid));

        var query = new GetSectorByIdQuery
        {
            Id = command.Id
        };

        var getSectorByIdResponse = await SendAsync(query);

        Assert.IsNotNull(getSectorByIdResponse);
        Assert.IsNotNull(getSectorByIdResponse.Sector);
    }
}