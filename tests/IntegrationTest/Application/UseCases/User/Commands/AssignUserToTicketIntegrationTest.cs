using Application.UseCases.Ticket.Queries;
using Application.UseCases.User.Commands.AssignUserToTicket;
using Application.UseCases.User.Queries;
using IntegrationTest.Application.UseCases.Ticket.Commands;

namespace IntegrationTest.Application.UseCases.User.Commands;

[TestClass]
public class AssignUserToTicketIntegrationTest : Testing
{
    [TestMethod]
    public async Task AssignUserToTicket()
    {
        var createUserIntegrationTest = new CreateUserIntegrationTest();
        _ = createUserIntegrationTest.CreateUser();

        var createTicketIntegrationTest = new CreateTicketIntegrationTest();
        _ = createTicketIntegrationTest.CreateTicket();

        var command = AssignUserToTicketCommandFactory();

        var assignUserToTicketResponse = await SendAsync(command);
        Assert.IsTrue(assignUserToTicketResponse.Success);

        var getTicketByIdQuery = new GetTicketByIdQuery
        {
            Id = CreateTicketIntegrationTest.CreatedTicket!.Id
        };

        var getTicketByIdResponse = await SendAsync(getTicketByIdQuery);
        Assert.IsNotNull(getTicketByIdResponse.Ticket);
    }

    [DataTestMethod]
    public static AssignUserToTicketCommand AssignUserToTicketCommandFactory()
    {
        var command = new AssignUserToTicketCommand
        {
            UserId = CreateUserIntegrationTest.CreatedUser!.Id,
            TicketId = CreateTicketIntegrationTest.CreatedTicket!.Id
        };

        return command;
    }
}