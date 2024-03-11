using Application;
using Application.UseCases.Answer.Queries;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Answer.Commands;
using IntegrationTest.Application.UseCases.Ticket.Commands;

namespace IntegrationTest.Application.UseCases.Answer.Queries;

[TestClass]
public class GetAnswerIntegrationTest : Testing
{

    [TestInitialize]
    public void TestInitialize()
    {
        var createAnswerIntegrationTest = new CreateAnswerIntegrationTest();
        _ = createAnswerIntegrationTest.CreateALotOfAnswers();
    }

    [TestMethod]
    public async Task GetAnswerByIdQuery()
    {
        var query = new GetAnswerByIdQuery
        {
            Id = CreateAnswerIntegrationTest.CreatedAnswer!.Id,
        };

        var getAnswerByIdResponse = await SendAsync(query);
        Assert.IsNotNull(getAnswerByIdResponse);
        Assert.IsTrue(getAnswerByIdResponse.Success);
    }

    [TestMethod]
    public async Task GetAnswersByTicketIdQuery()
    {
        var query = new GetAnswersByTicketIdQuery
        {
            TicketId = CreateTicketIntegrationTest.CreatedTicket!.Id,
        };

        var getAnswersByTicketIdResponse = await SendAsync(query);
        Assert.IsNotNull(getAnswersByTicketIdResponse);
        Assert.IsTrue(getAnswersByTicketIdResponse.Success);
    }
}
