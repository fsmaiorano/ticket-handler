using Application.UseCases.Ticket.Commands.CreateTicket;
using Application.UseCases.Ticket.Queries;
using Bogus;
using Domain.Constants;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Holder.Commands;
using IntegrationTest.Application.UseCases.Sector.Commands;
using IntegrationTest.Application.UseCases.User.Commands;

namespace IntegrationTest.Application.UseCases.Ticket.Commands;

[TestClass]
public class CreateTicketIntegrationTest : Testing
{
    public static TicketEntity? CreatedTicket;

    [TestInitialize]
    public void TestInitialize()
    {
        CreatedTicket = default;
    }

    [TestMethod]
    public async Task CreateTicket()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var createSectorIntegrationTest = new CreateSectorIntegrationTest();
        _ = createSectorIntegrationTest.CreateSector();

        var createUserIntegrationTest = new CreateUserIntegrationTest();
        _ = createUserIntegrationTest.CreateUser();

        var command = CreateTicketCommandFactory();

        var createdTicketResponse = await SendAsync(command);
        Assert.IsNotNull(createdTicketResponse);
        Assert.IsNotNull(createdTicketResponse.Ticket);
        Assert.IsInstanceOfType(createdTicketResponse.Ticket.Id, typeof(Guid));

        var storedTicket = await FindAsync<TicketEntity>(createdTicketResponse.Ticket.Id);
        CreatedTicket = storedTicket;
    }

    [TestMethod]
    public async Task CreateTicketMany()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var createSectorIntegrationTest = new CreateSectorIntegrationTest();
        _ = createSectorIntegrationTest.CreateSector();

        var createUserIntegrationTest = new CreateUserIntegrationTest();
        _ = createUserIntegrationTest.CreateUser();

        for (var i = 0; i < 150; i++)
        {
            var command = CreateTicketCommandFactory();
            var createdTicketResponse = await SendAsync(command);
            Assert.IsNotNull(createdTicketResponse);
            Assert.IsNotNull(createdTicketResponse.Ticket);
            Assert.IsInstanceOfType(createdTicketResponse.Ticket.Id, typeof(Guid));
        }
    }

    [DataTestMethod]
    public static CreateTicketCommand CreateTicketCommandFactory()
    {
        var command = new CreateTicketCommand
        {
            Title = new Faker().Lorem.Sentence(),
            Content = new Faker().Lorem.Paragraph(),
            Status = TicketStatus.Active,
            Priority = TicketPriority.Medium,
            UserId = CreateUserIntegrationTest.CreatedUser!.Id,
            HolderId = CreateHolderIntegrationTest.CreatedHolder!.Id,
            SectorId = CreateSectorIntegrationTest.CreatedSector!.Id,
            AssigneeId = CreateUserIntegrationTest.CreatedUser!.Id
        };

        return command;
    }
}
