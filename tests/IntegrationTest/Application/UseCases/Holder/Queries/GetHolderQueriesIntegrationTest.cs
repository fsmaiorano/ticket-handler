using Application.UseCases.Holder.Queries;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Holder.Commands;

namespace IntegrationTest.Application.UseCases.Holder.Queries;

[TestClass]
public class GetHolderQueriesIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestMethod]
    public async Task GetHolderByIdQuery()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var query = new GetHolderByIdQuery
        {
            Id = CreateHolderIntegrationTest.CreatedHolder!.Id,
        };

        var holder = await SendAsync(query);
        Assert.IsNotNull(holder);
        Assert.IsInstanceOfType(holder, typeof(HolderEntity));
    }
}
