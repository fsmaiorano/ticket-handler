using Application.UseCases.Holder.Commands.UpdateHolder;
using Application.UseCases.Holder.Queries;
using Bogus;

namespace IntegrationTest.Application.UseCases.Holder.Commands;

[TestClass]
public class UpdateHolderIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();
    }

    [TestMethod]
    public async Task UpdateHolder()
    {
        var holder = CreateHolderIntegrationTest.CreatedHolder;

        var command = new UpdateHolderCommand
        {
            Id = holder!.Id,
            Name = new Faker().Name.FullName()
        };

        var updatedHolderId = await SendAsync(command);

        Assert.IsNotNull(updatedHolderId);
        Assert.IsInstanceOfType(updatedHolderId, typeof(Guid));
        Assert.AreEqual(updatedHolderId, holder.Id);

        var query = new GetHolderByIdQuery
        {
            Id = command.Id
        };

        var updatedHolder = await SendAsync(query);

        Assert.IsNotNull(updatedHolder);
        Assert.AreEqual(command.Name, updatedHolder?.Name);
    }
}