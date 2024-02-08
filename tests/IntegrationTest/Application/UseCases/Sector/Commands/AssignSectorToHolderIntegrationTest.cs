using Application.UseCases.Holder.Queries;
using Application.UseCases.Sector.Commands.AssignSectorToHolder;
using IntegrationTest.Application.UseCases.Holder.Commands;

namespace IntegrationTest.Application.UseCases.Sector.Commands;

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

        var assignSectorToHolderResponse = await SendAsync(command);
        Assert.IsTrue(assignSectorToHolderResponse.Success);
        Assert.IsNotNull(assignSectorToHolderResponse.Holder);
        Assert.IsNotNull(assignSectorToHolderResponse.Sectors);
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