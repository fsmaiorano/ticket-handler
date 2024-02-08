using Application.UseCases.Answer.Commands.CreateAnswer;
using Application.UseCases.Answer.Queries;
using Bogus;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Holder.Commands;
using IntegrationTest.Application.UseCases.Sector.Commands;
using IntegrationTest.Application.UseCases.Ticket.Commands;
using IntegrationTest.Application.UseCases.User.Commands;

namespace IntegrationTest.Application.UseCases.Answer.Commands;

[TestClass]
public class CreateAnswerIntegrationTest : Testing
{
    public static AnswerEntity? CreatedAnswer;

    [TestInitialize]
    public void TestInitialize()
    {
        CreatedAnswer = default;
    }

    [TestMethod]
    public async Task CreateAnswer()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var createSectorIntegrationTest = new CreateSectorIntegrationTest();
        _ = createSectorIntegrationTest.CreateSector();

        var createUserIntegrationTest = new CreateUserIntegrationTest();
        _ = createUserIntegrationTest.CreateUser();

        var createTicketIntegrationTest = new CreateTicketIntegrationTest();
        _ = createTicketIntegrationTest.CreateTicket();

        var command = CreateAnswerCommandFactory();

        var createdAnswer = await SendAsync(command);
        Assert.IsNotNull(createdAnswer);
        Assert.IsNotNull(createdAnswer.Answer);

        var query = new GetAnswerByIdQuery
        {
            Id = createdAnswer.Answer.Id,
        };
      
        var getAnswerByIdResponse = await SendAsync(query);
        Assert.IsNotNull(getAnswerByIdResponse);
        Assert.IsTrue(getAnswerByIdResponse.Success);

        CreatedAnswer = getAnswerByIdResponse.Answer;
    }

    [DataTestMethod]
    public static CreateAnswerCommand CreateAnswerCommandFactory()
    {
        var command = new CreateAnswerCommand
        {
            Content = new Faker().Lorem.Sentence(),
            TicketId = CreateTicketIntegrationTest.CreatedTicket!.Id,
            UserId = CreateUserIntegrationTest.CreatedUser!.Id,
            HolderId = CreateHolderIntegrationTest.CreatedHolder!.Id,
            SectorId = CreateSectorIntegrationTest.CreatedSector!.Id,
        };

        return command;
    }
}
