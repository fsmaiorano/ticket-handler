using Application.UseCases.Sector.Commands.CreateSector;
using Application.UseCases.Sector.Queries;
using Bogus;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Holder.Commands;

namespace IntegrationTest.Application.UseCases.Sector.Commands;

[TestClass]
public class CreateSectorIntegrationTest : Testing
{
    public static SectorEntity? CreatedSector;

    [TestInitialize]
    public void TestInitialize()
    {
        CreatedSector = default;
    }

    [TestMethod]
    public async Task CreateSector()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var command = CreateSectorCommandFactory();

        var createdSectorResponse = await SendAsync(command);
        Assert.IsNotNull(createdSectorResponse);
        Assert.IsNotNull(createdSectorResponse.Sector);

        var query = new GetSectorByIdQuery
        {
            Id = createdSectorResponse.Sector.Id,
        };

        var getSectorByIdQueryResponse = await SendAsync(query);
        Assert.IsNotNull(getSectorByIdQueryResponse);
        Assert.IsNotNull(getSectorByIdQueryResponse.Sector);
        Assert.AreEqual(command.Name, getSectorByIdQueryResponse.Sector.Name);

        CreatedSector = getSectorByIdQueryResponse.Sector;
    }

    [DataTestMethod]
    public static CreateSectorCommand CreateSectorCommandFactory()
    {
        var command = new CreateSectorCommand
        {
            Name = new Faker().Name.FullName(),
            HolderId = CreateHolderIntegrationTest.CreatedHolder!.Id,
        };

        return command;
    }
}