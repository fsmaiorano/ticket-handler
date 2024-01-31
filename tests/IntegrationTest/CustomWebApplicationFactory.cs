using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTest;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var solutionPath = GetSolutionPath();
            var builder = new ConfigurationBuilder()
                .SetBasePath(solutionPath.FullName)
                .AddJsonFile("src/WebApi/appsettings.Development.json", optional: false)
                .Build();

            configurationBuilder.AddConfiguration(builder);
       });

        builder.ConfigureServices((builder, services) =>
        {
            services
                .Remove<DbContextOptions<DataContext>>()
                .AddDbContextFactory<DataContext>((sp, options) =>
                    options.UseInMemoryDatabase("InMemoryDb"));

            services.AddSingleton<IConfiguration>(builder.Configuration);
        });
    }

    internal static DirectoryInfo GetSolutionPath(string currentPath = null!)
    {
        var directory = new DirectoryInfo(
            currentPath ?? Directory.GetCurrentDirectory());
        while (directory != null && !directory.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }
        return directory!;
    }
}
