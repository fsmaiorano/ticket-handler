﻿using Application.UseCases.User.Commands.CreateUser;
using Application.UseCases.User.Queries;
using Bogus;
using Domain.Entities;

namespace IntegrationTest.Application.UseCases.User;

[TestClass]
public class CreateUserIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public static async Task<UserEntity?> CreateUser()
    {
        var command = CreateUserCommand();

        var createdUserId = await SendAsync(command);
        Assert.IsNotNull(createdUserId);
        Assert.IsInstanceOfType(createdUserId, typeof(Guid));

        var query = new GetUserByEmail
        {
            Email = command.Email
        };

        var user = await SendAsync(query);
        Assert.IsNotNull(user);
        Assert.AreEqual(command.Name, user?.Name);
        Assert.AreEqual(command.Email, user?.Email);
        Assert.AreEqual(command.Password, user?.Password);
        Assert.AreEqual(command.Username, user?.Username);

        return user;
    }

    [DataTestMethod]
    public static CreateUserCommand CreateUserCommand()
    {
        var command = new CreateUserCommand
        {
            Name = new Faker().Name.FullName(),
            Email = new Faker().Internet.Email(),
            Password = new Faker().Internet.Password(),
            Username = new Faker().Internet.UserName()
        };

        return command;
    }
}
