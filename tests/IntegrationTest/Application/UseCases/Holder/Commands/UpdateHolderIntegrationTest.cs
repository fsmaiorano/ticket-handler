using Application.UseCases.Holder.Commands.UpdateHolder;
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

        var updatedHolderResponse = await SendAsync(command);

        Assert.IsNotNull(updatedHolderResponse);
        Assert.IsTrue(updatedHolderResponse.Success);
    }
}