using Application.UseCases.Ticket.Commands.DeleteTicket;
using Application.UseCases.Ticket.Queries;

namespace IntegrationTest.Application.UseCases.Ticket.Commands;

[TestClass]
public class DeleteTicketIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
        var createTicketIntegrationTest = new CreateTicketIntegrationTest();
        _ = createTicketIntegrationTest.CreateTicket();
    }

    [TestMethod]
    public async Task DeleteTicket()
    {
        var sector = CreateTicketIntegrationTest.CreatedTicket;

        var command = new DeleteTicketCommand
        {
            Id = sector!.Id
        };

        var deletedTicketId = await SendAsync(command);
        Assert.IsNotNull(deletedTicketId);
        Assert.IsInstanceOfType(deletedTicketId, typeof(Guid));

        var query = new GetTicketByIdQuery
        {
            Id = sector.Id
        };

        var deletedTicket = await SendAsync(query);
        Assert.IsNull(deletedTicket);
    }
}
