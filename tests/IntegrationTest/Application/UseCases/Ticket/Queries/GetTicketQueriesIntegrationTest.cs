using System.Security.Cryptography;
using Application.UseCases.Ticket.Queries;
using IntegrationTest.Application.UseCases.Holder.Commands;
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
    public async Task GetTicketsByIdQuery()
    {
        var createTicketIntegrationTest = new CreateTicketIntegrationTest();
        _ = createTicketIntegrationTest.CreateTicket();

        var query = new GetTicketByIdQuery
        {
            Id = CreateTicketIntegrationTest.CreatedTicket!.Id,
        };

        var getTicketByIdResponse = await SendAsync(query);
        Assert.IsNotNull(getTicketByIdResponse);
        Assert.IsTrue(getTicketByIdResponse.Success);
    }

    [TestMethod]
    public async Task GetTicketsBySectorIdQuery()
    {
        var createTicketIntegrationTest = new CreateTicketIntegrationTest();
        _ = createTicketIntegrationTest.CreateTicket();

        var query = new GetTicketsBySectorIdQuery
        {
            HolderId = CreateTicketIntegrationTest.CreatedTicket!.HolderId,
            SectorId = CreateTicketIntegrationTest.CreatedTicket!.SectorId,
        };

        var getTicketByIdResponse = await SendAsync(query);
        Assert.IsNotNull(getTicketByIdResponse);
        Assert.IsTrue(getTicketByIdResponse.Success);
    }

    [TestMethod]
    public async Task GetTicketsByHolderIdQuery()
    {
        var createTicketIntegrationTest = new CreateTicketIntegrationTest();
        _ = createTicketIntegrationTest.CreateTicketMany();

        var query = new GetTicketsByHolderIdQuery
        {
            HolderId = CreateHolderIntegrationTest.CreatedHolder!.Id,
            PageNumber = 1,
            PageSize = 20,
        };

        var getTicketByIdResponse = await SendAsync(query);
        Assert.IsNotNull(getTicketByIdResponse);
        Assert.IsTrue(getTicketByIdResponse.Success);
    }

    [TestMethod]
    public async Task GetTicketsByAssigneeIdQuery()
    {
        var createTicketIntegrationTest = new CreateTicketIntegrationTest();
        _ = createTicketIntegrationTest.CreateTicket();

        var query = new GetTicketsByAssigneeIdQuery
        {
            AssigneeId = (Guid)CreateTicketIntegrationTest.CreatedTicket!.AssigneeId!,
        };

        var getTicketByIdResponse = await SendAsync(query);
        Assert.IsNotNull(getTicketByIdResponse);
        Assert.IsTrue(getTicketByIdResponse.Success);
    }
}
