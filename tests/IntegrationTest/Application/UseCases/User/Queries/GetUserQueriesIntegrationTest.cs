using Application.UseCases.User.Queries;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Holder.Commands;
using IntegrationTest.Application.UseCases.Sector.Commands;
using IntegrationTest.Application.UseCases.User.Commands;

namespace IntegrationTest.Application.UseCases.User.Queries;

[TestClass]
public class GetUserQueriesIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestMethod]
    public async Task GetUsersByHolderIdQuery()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var createSectorIntegrationTest = new CreateSectorIntegrationTest();
        _ = createSectorIntegrationTest.CreateSector();

        var createUserIntegrationTest = new CreateUserIntegrationTest();
        _ = createUserIntegrationTest.CreateUser();

        var query = new GetUsersByHolderIdQuery
        {
            HolderId = CreateHolderIntegrationTest.CreatedHolder!.Id,
        };

        var users = await SendAsync(query);
        Assert.IsNotNull(users);
        Assert.IsInstanceOfType(users, typeof(List<UserEntity>));
        Assert.IsTrue(users!.Count > 0);
    }

    [TestMethod]
    public async Task GetUsersByHolderIdQueryEmptyList()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var createSectorIntegrationTest = new CreateSectorIntegrationTest();
        _ = createSectorIntegrationTest.CreateSector();

        var query = new GetUsersByHolderIdQuery
        {
            HolderId = CreateHolderIntegrationTest.CreatedHolder!.Id,
        };

        var users = await SendAsync(query);
        Assert.IsNotNull(users);
        Assert.IsInstanceOfType(users, typeof(List<UserEntity>));
        Assert.IsTrue(users!.Count == 0);
    }
}

