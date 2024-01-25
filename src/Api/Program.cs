using Application;
using Application.Common.Models;
using Application.UseCases.User.Commands.CreateUser;
using FluentValidation;
using Infrastructure;
using MediatR;
using System;
using System.Text.Json.Serialization;
using WebApi;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebApiServices();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");

app.UseSwagger();
app.UseSwaggerUI(
    options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticket Handler API");
        options.RoutePrefix = string.Empty;
    }
);

app.UseHttpsRedirection();

var mediator = app.Services.GetService(typeof(ISender)) as ISender ?? throw new NullReferenceException("Mediator is null");

app.MapPost("/users", async (User input) =>
{
    try
    {
        var command = new CreateUserCommand()
        {
            Name = input.Name ?? string.Empty,
            Email = input.Email ?? string.Empty,
            Password = input.Password ?? string.Empty,
            Username = input.Username ?? string.Empty
        };

        var result = await mediator.Send(command);
        return result is not null ? Results.Ok(result) : Results.BadRequest();

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.BadRequest();
    }

}).WithName("CreateUser").WithOpenApi();

app.Run();

public partial class Program { }