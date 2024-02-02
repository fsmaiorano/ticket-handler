using Application.UseCases.Answer.Commands.DeleteAnswer;
using Application.UseCases.Answer.Queries;

namespace IntegrationTest.Application.UseCases.Answer.Commands;

[TestClass]
public class DeleteAnswerIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {
        var createUserIntegrationTest = new CreateAnswerIntegrationTest();
        _ = createUserIntegrationTest.CreateAnswer();
    }


    [TestMethod]
    public async Task DeleteAnswer()
    {
        var holder = CreateAnswerIntegrationTest.CreatedAnswer;

        var command = new DeleteAnswerCommand
        {
            Id = holder!.Id
        };

        var deletedAnswerId = await SendAsync(command);
        Assert.IsNotNull(deletedAnswerId);
        Assert.IsInstanceOfType(deletedAnswerId, typeof(Guid));

        var query = new GetAnswerByIdQuery
        {
            Id = holder.Id
        };

        var deletedAnswer = await SendAsync(query);
        Assert.IsNull(deletedAnswer);
    }
}
