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

        var createdSectorId = await SendAsync(command);
        Assert.IsNotNull(createdSectorId);
        Assert.IsInstanceOfType(createdSectorId, typeof(Guid));

        var query = new GetSectorByIdQuery
        {
            Id = (Guid)createdSectorId,
        };

        var createdSector = await SendAsync(query);
        Assert.IsNotNull(createdSector);
        Assert.AreEqual(command.Name, createdSector?.Name);

        CreatedSector = createdSector;
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