using Application.UseCases.Ticket.Queries;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Ticket.Commands;

namespace IntegrationTest.Application.UseCases.Ticket.Queries;

[TestClass]
public class GetTicketQueriesIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestMethod]
    public async Task GetTicketsBySectorIdQuery()
    {
        var createTicketIntegrationTest = new CreateTicketIntegrationTest();
        _ = createTicketIntegrationTest.CreateTicket();

        var query = new GetTicketByIdQuery
        {
            Id = CreateTicketIntegrationTest.CreatedTicket!.Id,
        };

        var tickets = await SendAsync(query);
        Assert.IsNotNull(tickets);
        Assert.IsInstanceOfType(tickets, typeof(TicketEntity));
    }
}
