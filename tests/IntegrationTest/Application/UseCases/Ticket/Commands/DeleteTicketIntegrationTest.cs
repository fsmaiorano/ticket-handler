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

        var deletedTicketResponse = await SendAsync(command);
        Assert.IsNotNull(deletedTicketResponse);
        Assert.IsTrue(deletedTicketResponse.Success);
    }
}
