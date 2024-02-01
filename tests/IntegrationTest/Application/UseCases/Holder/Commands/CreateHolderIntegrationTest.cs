using Application.UseCases.Holder.Commands.CreateHolder;
using Application.UseCases.Holder.Queries;
using Bogus;
using Domain.Entities;

namespace IntegrationTest.Application.UseCases.Holder.Commands;

[TestClass]
public class CreateHolderIntegrationTest : Testing
{
    public static HolderEntity? CreatedHolder;

    [TestInitialize]
    public void TestInitialize()
    {
        CreatedHolder = default;
    }

    [TestMethod]
    public async Task CreateHolder()
    {
        var command = CreateHolderCommandFactory();

        var createdHolderId = await SendAsync(command);
        Assert.IsNotNull(createdHolderId);
        Assert.IsInstanceOfType(createdHolderId, typeof(Guid));

        var query = new GetHolderByIdQuery
        {
            Id = (Guid)createdHolderId,
        };

        var createdHolder = await SendAsync(query);
        Assert.IsNotNull(createdHolder);
        Assert.AreEqual(command.Name, createdHolder?.Name);

        CreatedHolder = createdHolder;
    }

    [DataTestMethod]
    public static CreateHolderCommand CreateHolderCommandFactory()
    {
        var command = new CreateHolderCommand
        {
            Name = new Faker().Name.FullName(),
        };

        return command;
    }
}