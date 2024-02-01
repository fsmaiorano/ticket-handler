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

        var createdTicketId = await SendAsync(command);
        Assert.IsNotNull(createdTicketId);
        Assert.IsInstanceOfType(createdTicketId, typeof(Guid));

        var query = new GetTicketByIdQuery
        {

            Id = (Guid)createdTicketId
        };

        var ticket = await SendAsync(query);
        Assert.IsNotNull(ticket);
        Assert.AreEqual(command.Title, ticket?.Title);
        Assert.AreEqual(command.Content, ticket?.Content);
        Assert.AreEqual(command.Status, ticket?.Status);
        Assert.AreEqual(command.Priority, ticket?.Priority);
        Assert.AreEqual(command.HolderId, ticket?.HolderId);
        Assert.AreEqual(command.SectorId, ticket?.SectorId);
        Assert.AreEqual(command.AssigneeId, ticket?.AssigneeId);

        CreatedTicket = ticket;
    }

    [DataTestMethod]
    public static CreateTicketCommand CreateTicketCommandFactory()
    {
        var command = new CreateTicketCommand
        {
            Title = new Faker().Lorem.Sentence(),
            Content = new Faker().Lorem.Paragraph(),
            Status = new Faker().PickRandom<TicketStatus>(),
            Priority = new Faker().PickRandom<TicketPriority>(),
            HolderId = CreateHolderIntegrationTest.CreatedHolder!.Id,
            SectorId = CreateSectorIntegrationTest.CreatedSector!.Id,
            AssigneeId = CreateUserIntegrationTest.CreatedUser!.Id
        };

        return command;
    }
}
