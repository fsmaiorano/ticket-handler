using Application.UseCases.Answer.Commands.CreateAnswer;
using Bogus;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Holder.Commands;
using IntegrationTest.Application.UseCases.Sector.Commands;
using IntegrationTest.Application.UseCases.Ticket.Commands;
using IntegrationTest.Application.UseCases.User.Commands;
using Newtonsoft.Json;

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
        var x = "[{'WS_COD_CARTAO_M':'4170056000087757','WS_PIPE15':'|','WS_COD_CAMPANHA_M':'000060','WS_PIPE16':'|','WS_DESC_CAMPANHA_M':'MEALHEIRO','WS_PIPE18':'0','WS_DATA_DETALHE_M':'20231211','WS_TARGA_KEY_M':'000000324202011','WS_PIPE17':'|','WS_DESCRICAO_MOVIMENTO_M':'OPCAO MEALHEIRO 01/12','WS_PIPE19':'|','WS_TIPO_MOVIMENTO_M':'C','WS_PIPE20':'|','WS_NUM_CART_ORIGIN_M':'4170056000087757','WS_PIPE21':'|','WS_PTS_CALC_CAMP_M':'00005033','WS_PTS_ATRIB_CAMP_M':'00005033','WS_PIPE23':'|','WS_COD_PARTNER_M':'000000000','WS_PIPE23A':'|','WS_DESC_PARTNER_M':'','WS_PIPE24':'|','WS_PTS_CALC_PART_M':'00000000','WS_PIPE25':'|','WS_PTS_ATRIB_PART_M':'00000000','WS_PIPE26':'|','WS_PONTOS_REBATIDOS_M':'00000000','WS_TIMESTAMP_M':'23121206201458','WS_PIPE26A':'|','WS_MONTANTE_M':'000005033'},{'WS_COD_CARTAO_M':'',				   'WS_PIPE15':'', 'WS_COD_CAMPANHA_M':'',      'WS_PIPE16':'', 'WS_DESC_CAMPANHA_M':'','WS_PIPE18':'','WS_DATA_DETALHE_M':'','WS_TARGA_KEY_M':'','WS_PIPE17':'','WS_DESCRICAO_MOVIMENTO_M':'','WS_PIPE19':'','WS_TIPO_MOVIMENTO_M':'','WS_PIPE20':'','WS_NUM_CART_ORIGIN_M':'','WS_PIPE21':'','WS_PTS_CALC_CAMP_M':'','WS_PTS_ATRIB_CAMP_M':'','WS_PIPE23':'','WS_COD_PARTNER_M':'','WS_PIPE23A':'','WS_DESC_PARTNER_M':'','WS_PIPE24':'','WS_PTS_CALC_PART_M':'','WS_PIPE25':'','WS_PTS_ATRIB_PART_M':'','WS_PIPE26':'','WS_PONTOS_REBATIDOS_M':'','WS_TIMESTAMP_M':'','WS_PIPE26A':'','WS_MONTANTE_M':''},{'WS_COD_CARTAO_M':'','WS_PIPE15':'','WS_COD_CAMPANHA_M':'','WS_PIPE16':'','WS_DESC_CAMPANHA_M':'','WS_PIPE18':'','WS_DATA_DETALHE_M':'','WS_TARGA_KEY_M':'','WS_PIPE17':'','WS_DESCRICAO_MOVIMENTO_M':'','WS_PIPE19':'','WS_TIPO_MOVIMENTO_M':'','WS_PIPE20':'','WS_NUM_CART_ORIGIN_M':'','WS_PIPE21':'','WS_PTS_CALC_CAMP_M':'','WS_PTS_ATRIB_CAMP_M':'','WS_PIPE23':'','WS_COD_PARTNER_M':'','WS_PIPE23A':'','WS_DESC_PARTNER_M':'','WS_PIPE24':'','WS_PTS_CALC_PART_M':'','WS_PIPE25':'','WS_PTS_ATRIB_PART_M':'','WS_PIPE26':'','WS_PONTOS_REBATIDOS_M':'','WS_TIMESTAMP_M':'','WS_PIPE26A':'','WS_MONTANTE_M':''},{'WS_COD_CARTAO_M':'','WS_PIPE15':'','WS_COD_CAMPANHA_M':'','WS_PIPE16':'','WS_DESC_CAMPANHA_M':'','WS_PIPE18':'','WS_DATA_DETALHE_M':'','WS_TARGA_KEY_M':'','WS_PIPE17':'','WS_DESCRICAO_MOVIMENTO_M':'','WS_PIPE19':'','WS_TIPO_MOVIMENTO_M':'','WS_PIPE20':'','WS_NUM_CART_ORIGIN_M':'','WS_PIPE21':'','WS_PTS_CALC_CAMP_M':'','WS_PTS_ATRIB_CAMP_M':'','WS_PIPE23':'','WS_COD_PARTNER_M':'','WS_PIPE23A':'','WS_DESC_PARTNER_M':'','WS_PIPE24':'','WS_PTS_CALC_PART_M':'','WS_PIPE25':'','WS_PTS_ATRIB_PART_M':'','WS_PIPE26':'','WS_PONTOS_REBATIDOS_M':'','WS_TIMESTAMP_M':'','WS_PIPE26A':'','WS_MONTANTE_M':''},{'WS_COD_CARTAO_M':'','WS_PIPE15':'','WS_COD_CAMPANHA_M':'','WS_PIPE16':'','WS_DESC_CAMPANHA_M':'','WS_PIPE18':'','WS_DATA_DETALHE_M':'','WS_TARGA_KEY_M':'','WS_PIPE17':'','WS_DESCRICAO_MOVIMENTO_M':'','WS_PIPE19':'','WS_TIPO_MOVIMENTO_M':'','WS_PIPE20':'','WS_NUM_CART_ORIGIN_M':'','WS_PIPE21':'','WS_PTS_CALC_CAMP_M':'','WS_PTS_ATRIB_CAMP_M':'','WS_PIPE23':'','WS_COD_PARTNER_M':'','WS_PIPE23A':'','WS_DESC_PARTNER_M':'','WS_PIPE24':'','WS_PTS_CALC_PART_M':'','WS_PIPE25':'','WS_PTS_ATRIB_PART_M':'','WS_PIPE26':'','WS_PONTOS_REBATIDOS_M':'','WS_TIMESTAMP_M':'','WS_PIPE26A':'','WS_MONTANTE_M':''}]";
        var xx = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(x));


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
