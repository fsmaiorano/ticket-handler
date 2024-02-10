using Application.UseCases.Ticket.Commands.UpdateTicket;
using Application.UseCases.Ticket.Queries;
using Domain.Entities;

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
            AssigneeId = ticket.AssigneeId,
            Priority = ticket.Priority,
            Status = ticket.Status
        };

        var updatedTicketResponse = await SendAsync(command);

        Assert.IsNotNull(updatedTicketResponse);
        Assert.IsTrue(updatedTicketResponse.Success);

        var storedTicket = await FindAsync<TicketEntity>(ticket.Id);
        Assert.IsNotNull(storedTicket);
        Assert.AreEqual(command.Title, storedTicket.Title);
        Assert.AreEqual(command.Content, storedTicket.Content);
    }
}
