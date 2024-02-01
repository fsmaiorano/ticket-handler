using Application.UseCases.Sector.Queries;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Holder.Commands;
using IntegrationTest.Application.UseCases.Sector.Commands;

namespace IntegrationTest.Application.UseCases.Sector.Queries;

[TestClass]
public class GetSectorQueriesIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestMethod]
    public async Task GetSectorsByHolderIdQuery()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var createSectorIntegrationTest = new CreateSectorIntegrationTest();
        _ = createSectorIntegrationTest.CreateSector();
        _ = createSectorIntegrationTest.CreateSector();
        _ = createSectorIntegrationTest.CreateSector();
        _ = createSectorIntegrationTest.CreateSector();

        var query = new GetSectorsByHolderIdQuery
        {
            HolderId = CreateHolderIntegrationTest.CreatedHolder!.Id,
        };

        var sectors = await SendAsync(query);
        Assert.IsNotNull(sectors);
        Assert.IsInstanceOfType(sectors, typeof(List<SectorEntity>));
    }
}

