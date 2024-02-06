using Application.UseCases.Authentication.Commands.SignIn;
using Application.UseCases.Authentication.Commands.SignUp;
using Application.UseCases.Holder.Queries;
using Application.UseCases.User.Queries;
using Bogus;
using Domain.Constants;
using Domain.Entities;
using IntegrationTest.Application.UseCases.Holder.Commands;
using IntegrationTest.Application.UseCases.User.Commands;

namespace IntegrationTest.Application.UseCases.Authentication.Commands;

[TestClass]
public class AuthenticationIntegrationTest : Testing
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
    public async Task SignUp()
    {
        var createHolderIntegrationTest = new CreateHolderIntegrationTest();
        _ = createHolderIntegrationTest.CreateHolder();

        var createUserIntegrationTest = new CreateUserIntegrationTest();
        _ = createUserIntegrationTest.CreateUser();

        var command = SignUpCommandFactory();

        var signUpResponse = await SendAsync(command);
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

    [TestMethod]
    public async Task SignIn()
    {
        await SignUp();

        var command = new SignInCommand
        {
            Email = CreatedUser?.Email ?? string.Empty,
            Password = CreatedUser?.Password ?? string.Empty,
        };

        var token = await SendAsync(command);
        Assert.IsNotNull(token);
    }

    [DataTestMethod]
    public static SignUpCommand SignUpCommandFactory()
    {
        var command = new SignUpCommand
        {
            HolderName = new Faker().Name.FullName(),
            FullName = new Faker().Name.FullName(),
            Email = new Faker().Internet.Email(),
            Password = new Faker().Internet.Password(),
        };

        return command;
    }
}
