using Application.UseCases.Answer.Commands.CreateAnswer;
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

        var storedAnswer = await FindAsync<AnswerEntity>(createdAnswer.Answer.Id);
        CreatedAnswer = storedAnswer;
    }

    [TestMethod]
    public async Task CreateALotOfAnswers()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var createSectorIntegrationTest = new CreateSectorIntegrationTest();
        _ = createSectorIntegrationTest.CreateSector();

        var createUserIntegrationTest = new CreateUserIntegrationTest();
        _ = createUserIntegrationTest.CreateUser();

        var createTicketIntegrationTest = new CreateTicketIntegrationTest();
        _ = createTicketIntegrationTest.CreateTicket();

        for (var i = 1; i <= 7; i++)
        {
            var command = CreateAnswerCommandFactory();
            _ = await SendAsync(command);
        }

        var storedTickets = await CountAsync<AnswerEntity>();
        Assert.AreEqual(7, storedTickets);
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
