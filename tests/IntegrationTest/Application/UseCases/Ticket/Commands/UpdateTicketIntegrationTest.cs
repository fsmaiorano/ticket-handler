using Application.UseCases.Ticket.Commands.UpdateTicket;
using Application.UseCases.Ticket.Queries;

namespace IntegrationTest.Application.UseCases.Ticket.Commands;

[TestClass]
public class UpdateTicketIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
        var createTicketIntegrationTest = new CreateTicketIntegrationTest();
        _ = createTicketIntegrationTest.CreateTicket();
    }

    [TestMethod]
    public async Task UpdateTicket()
    {
        var ticket = CreateTicketIntegrationTest.CreatedTicket;

        var command = new UpdateTicketCommand
        {
            Id = ticket!.Id,
            Title = "Updated Title",
            Content = "Updated Content",
            HolderId = ticket.HolderId,
            SectorId = ticket.SectorId,
            AssigneeId = ticket.AssigneeId
        };

        var updatedTicketId = await SendAsync(command);

        Assert.IsNotNull(updatedTicketId);
        Assert.IsInstanceOfType(updatedTicketId, typeof(Guid));
        Assert.AreEqual(updatedTicketId, ticket.Id);

        var query = new GetTicketByIdQuery
        {
            Id = command.Id
        };

        var updatedTicket = await SendAsync(query);

        Assert.IsNotNull(updatedTicket);
        Assert.AreEqual(command.Title, updatedTicket?.Title);
        Assert.AreEqual(command.Content, updatedTicket?.Content);
        Assert.AreEqual(command.Status, updatedTicket?.Status);
        Assert.AreEqual(command.Priority, updatedTicket?.Priority);
        Assert.AreEqual(command.HolderId, updatedTicket?.HolderId);
        Assert.AreEqual(command.SectorId, updatedTicket?.SectorId);
        Assert.AreEqual(command.AssigneeId, updatedTicket?.AssigneeId);
    }
}
