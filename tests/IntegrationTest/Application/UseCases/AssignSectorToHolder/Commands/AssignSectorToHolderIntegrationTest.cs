using Application.UseCases.Holder.Queries;
using Application.UseCases.Sector.Commands.AssignSectorToHolder.Commands;
using IntegrationTest.Application.UseCases.Holder.Commands;
using IntegrationTest.Application.UseCases.Sector.Commands;

namespace IntegrationTest.Application.UseCases.AssignSectorToHolder.Commands;

[TestClass]
public class AssignSectorToHolderIntegrationTest : Testing
{
    [TestMethod]
    public async Task AssignSectorToHolder()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var createSectorIntegrationTest = new CreateSectorIntegrationTest();
        _ = createSectorIntegrationTest.CreateSector();

        var command = AssignSectorToHolderCommandFactory();

        var assigned = await SendAsync(command);
        Assert.IsTrue(assigned);

        var holder = new GetHolderByIdQuery { Id = CreateHolderIntegrationTest.CreatedHolder!.Id };

        var storedHolder = await SendAsync(holder);
        Assert.IsNotNull(storedHolder);
        Assert.IsTrue(storedHolder.Sectors!.Count > 0);
        Assert.AreEqual(CreateSectorIntegrationTest.CreatedSector!.Id, storedHolder.Sectors[0].Id);
    }

    [DataTestMethod]
    public static AssignSectorToHolderCommand AssignSectorToHolderCommandFactory()
    {
        var command = new AssignSectorToHolderCommand
        {
            HolderId = CreateHolderIntegrationTest.CreatedHolder!.Id,
            SectorsId = [CreateSectorIntegrationTest.CreatedSector!.Id]
        };

        return command;
    }
}