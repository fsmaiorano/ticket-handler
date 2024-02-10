using Application.UseCases.Authentication.Commands.SignIn;
using Application.UseCases.Authentication.Commands.SignUp;
using Application.UseCases.Holder.Queries;
using Application.UseCases.User.Queries;
using Bogus;
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
        Assert.IsNotNull(signUpResponse.CreatedHolder);

        var createdUser = await FindAsync<UserEntity>(signUpResponse.CreatedUser.Id);
        var createdHolder = await FindAsync<HolderEntity>(signUpResponse.CreatedHolder.Id);
    

        CreatedUser = createdUser;
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
