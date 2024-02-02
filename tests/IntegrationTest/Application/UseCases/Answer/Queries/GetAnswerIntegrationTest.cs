using Application.UseCases.Answer.Queries;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Answer.Commands;

namespace IntegrationTest.Application.UseCases.Answer.Queries;

[TestClass]
public class GetAnswerIntegrationTest : Testing
{

    [TestInitialize]
    public void TestInitialize()
    {
        var createAnswerIntegrationTest = new CreateAnswerIntegrationTest();
        _ = createAnswerIntegrationTest.CreateAnswer();
    }

    [TestMethod]
    public async Task GetAnswerByIdQuery()
    {
        var query = new GetAnswerByIdQuery
        {
            Id = CreateAnswerIntegrationTest.CreatedAnswer!.Id,
        };

        var answer = await SendAsync(query);
        Assert.IsNotNull(answer);
        Assert.IsInstanceOfType(answer, typeof(AnswerEntity));
    }
}
