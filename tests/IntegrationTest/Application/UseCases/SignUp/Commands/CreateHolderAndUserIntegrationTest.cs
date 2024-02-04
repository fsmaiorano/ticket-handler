using Application.UseCases.Holder.Queries;
using Application.UseCases.SignUp.Commands.CreateHolderAndUser;
using Application.UseCases.User.Queries;
using Bogus;
using Domain.Constants;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Holder.Commands;
using IntegrationTest.Application.UseCases.User.Commands;

namespace IntegrationTest.Application.UseCases.SignUp.Commands;

[TestClass]
public class CreateHolderAndUserIntegrationTest : Testing
{
    public static UserEntity? CreatedUser;
    public static HolderEntity? CreatedHolder;

    [TestInitialize]
    public void TestInitialize()
    {
        CreatedUser = default;
        CreatedHolder = default;
    }

    [TestMethod]
    public async Task CreateHolderAndUser()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var createUserIntegrationTest = new CreateUserIntegrationTest();
        _ = createUserIntegrationTest.CreateUser();

        var command = CreateHolderAndUserCommandFactory();

        var signUpResponse = await SendAsync<CreateHolderAndUserResponse>(command);
        Assert.IsNotNull(signUpResponse);
        Assert.IsNotNull(signUpResponse.CreatedUser);

        var query = new GetUserByIdQuery
        {
            Id = signUpResponse.CreatedUser,
        };

        var createdUser = await SendAsync(query);
        Assert.IsNotNull(createdUser);
        Assert.AreEqual(command.FullName, createdUser?.Name);
        Assert.AreEqual(createdUser?.Role, UserRoles.Administrator);

        CreatedUser = createdUser;

        var getHolderByIdQuery = new GetHolderByIdQuery
        {
            Id = signUpResponse.CreatedHolder,
        };

        var createdHolder = await SendAsync(getHolderByIdQuery);
        Assert.IsNotNull(createdHolder);
        Assert.AreEqual(command.HolderName, createdHolder?.Name);

        CreatedHolder = createdHolder;
    }

    [DataTestMethod]
    public static CreateHolderAndUserCommand CreateHolderAndUserCommandFactory()
    {
        var command = new CreateHolderAndUserCommand
        {
            HolderName = new Faker().Name.FullName(),
            FullName = new Faker().Name.FullName(),
            Email = new Faker().Internet.Email(),
            Password = new Faker().Internet.Password(),
        };

        return command;
    }
}
