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

        var createHolderResult = await SendAsync(command);
        Assert.IsNotNull(createHolderResult);
        Assert.IsNotNull(createHolderResult.Holder);

        var storedHolder = await FindAsync<HolderEntity>(createHolderResult.Holder.Id);
        CreatedHolder = storedHolder;
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