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
        var answer = CreateAnswerIntegrationTest.CreatedAnswer;

        var command = new DeleteAnswerCommand
        {
            Id = answer!.Id
        };

        var deletedAnswerId = await SendAsync(command);
        Assert.IsNotNull(deletedAnswerId);
        Assert.IsTrue(deletedAnswerId.Success);
    }
}
