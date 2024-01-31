using Application.UseCases.Holder.Commands;
using Application.UseCases.Holder.Queries;
using Bogus;
using Domain.Entities;

namespace IntegrationTest.Application.UseCases.Holder;

[TestClass]
public class CreateHolderIntegrationTest : Testing
{
    public static HolderEntity? CreatedHolder;

    [TestMethod]
    public async Task CreateHolder()
    {
        var command = CreateHolderCommand();

        var createdHolderId = await SendAsync(command);
        Assert.IsNotNull(createdHolderId);
        Assert.IsInstanceOfType(createdHolderId, typeof(Guid));

        var query = new GetHolderByIdQuery
        {
            Id = (Guid)createdHolderId
        };

        var holder = await SendAsync(query);
        Assert.IsNotNull(holder);
        Assert.AreEqual(command.Name, holder?.Name);

        CreatedHolder = holder;
    }

    [DataTestMethod]
    public static CreateHolderCommand CreateHolderCommand()
    {
        var command = new CreateHolderCommand
        {
            Name = new Faker().Name.FullName(),
        };

        return command;
    }
}