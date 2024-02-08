using Application.UseCases.Holder.Commands.DeleteHolder;

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

        var deletedHolderResponse = await SendAsync(command);
        Assert.IsNotNull(deletedHolderResponse);
        Assert.IsTrue(deletedHolderResponse.Success);
    }
}
