using Application.UseCases.Answer.Commands.UpdateAnswer;
using Application.UseCases.Answer.Queries;
using Bogus;

namespace IntegrationTest.Application.UseCases.Answer.Commands;

[TestClass]
public class UpdateAnswerIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
        var createAnswerIntegrationTest = new CreateAnswerIntegrationTest();
        _ = createAnswerIntegrationTest.CreateAnswer();
    }

    [TestMethod]
    public async Task UpdateAnswer()
    {
        var answer = CreateAnswerIntegrationTest.CreatedAnswer;

        var command = new UpdateAnswerCommand
        {
            Id = answer!.Id,
            Content = new Faker().Lorem.Sentence(),
            TicketId = answer.TicketId,
            UserId = answer.UserId,
            HolderId = answer.HolderId,
            SectorId = answer.SectorId
        };

        var updatedAnswerId = await SendAsync(command);

        Assert.IsNotNull(updatedAnswerId);
        Assert.IsInstanceOfType(updatedAnswerId, typeof(Guid));

        var query = new GetAnswerByIdQuery
        {
            Id = command.Id
        };

        var updatedAnswer = await SendAsync(query);

        Assert.IsNotNull(updatedAnswer);
    }
}