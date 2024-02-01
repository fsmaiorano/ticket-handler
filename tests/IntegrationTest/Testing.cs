using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace IntegrationTest;

public class Testing
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;

    //public readonly IAuthService _authService;

    public Testing()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        _factory = new CustomWebApplicationFactory();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

        using var scope = _scopeFactory.CreateScope();
        //_authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
   where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    //public async Task<HttpClient> CreateHttpClient()
    //{
    //    using var application = new CustomWebApplicationFactory();
    //    using var client = application.CreateClient();

    //    //    var user = new UserAuthenticationDto
    //    //    {
    //    //        Id = 1,
    //    //        Name = "Test",
    //    //        Email = "test@test.com"
    //    //    };

    //    //    client.DefaultRequestHeaders.Accept.Clear();
    //    //    client.BaseAddress = new Uri("http://localhost:5000");
    //    //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GenerateToken(user));

    //    return client;
    //}

    //public async Task<HttpResponseMessage> PostAsync(string url, object data)
    //{
    //    using var client = await CreateHttpClient();
    //    return await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(data)));
    //}

    //public async Task<HttpResponseMessage> PutAsync(string url, object data)
    //{
    //    using var client = await CreateHttpClient();
    //    return await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(data)));
    //}

    //public async Task<HttpResponseMessage> DeleteAsync(string url)
    //{
    //    using var client = await CreateHttpClient();
    //    return await client.DeleteAsync(url);
    //}

    //public async Task<HttpResponseMessage> GetAsync(string url)
    //{
    //    using var client = await CreateHttpClient();
    //    return await client.GetAsync(url);
    //}
}
