using Application.UseCases.Holder.Queries;
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

        var getHolderByIdResponse = await SendAsync(query);
        Assert.IsNotNull(getHolderByIdResponse);
        Assert.IsTrue(getHolderByIdResponse.Success);
    }
}
