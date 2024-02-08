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
            AssigneeId = ticket.AssigneeId,
            Priority = ticket.Priority,
            Status = ticket.Status
        };

        var updatedTicketResponse = await SendAsync(command);

        Assert.IsNotNull(updatedTicketResponse);
        Assert.IsTrue(updatedTicketResponse.Success);

        var query = new GetTicketByIdQuery
        {
            Id = command.Id
        };

         var getTicketByIdResponse = await SendAsync(query);
        Assert.IsNotNull(getTicketByIdResponse);
        Assert.IsNotNull(getTicketByIdResponse.Ticket);
        Assert.AreEqual(command.Title, getTicketByIdResponse.Ticket.Title);
        Assert.AreEqual(command.Content, getTicketByIdResponse.Ticket.Content);
        Assert.AreEqual(command.Status, getTicketByIdResponse.Ticket.Status);
        Assert.AreEqual(command.Priority, getTicketByIdResponse.Ticket.Priority);
        Assert.AreEqual(command.HolderId, getTicketByIdResponse.Ticket.HolderId);
        Assert.AreEqual(command.SectorId, getTicketByIdResponse.Ticket.SectorId);
        Assert.AreEqual(command.AssigneeId, getTicketByIdResponse.Ticket.AssigneeId);
    }
}
