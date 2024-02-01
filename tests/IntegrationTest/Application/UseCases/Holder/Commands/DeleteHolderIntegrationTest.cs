using Application.UseCases.Holder.Commands.DeleteHolder;
using Application.UseCases.Holder.Queries;

namespace IntegrationTest.Application.UseCases.Holder.Commands;

[TestClass]
public class DeleteHolderIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
        var createUserIntegrationTest = new CreateHolderIntegrationTest();
        _ = createUserIntegrationTest.CreateHolder();
    }


    [TestMethod]
    public async Task DeleteHolder()
    {
        var holder = CreateHolderIntegrationTest.CreatedHolder;

        var command = new DeleteHolderCommand
        {
            Id = holder!.Id
        };

        var deletedHolderId = await SendAsync(command);
        Assert.IsNotNull(deletedHolderId);
        Assert.IsInstanceOfType(deletedHolderId, typeof(Guid));

        var query = new GetHolderByIdQuery
        {
            Id = holder.Id
        };

        var deletedHolder = await SendAsync(query);
        Assert.IsNull(deletedHolder);
    }
}
