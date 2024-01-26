using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure.Context;

public class DataContext : DbContext, IDataContext
{
    // private readonly IConfiguration _configuration;
    // private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public DataContext()
    {

    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DataContext(
       DbContextOptions<DataContext> options,
       IMediator mediator,
       IConfiguration configuration,
       AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        // _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        // _configuration = configuration;
    }

    //public DbSet<PokemonEntity> Pokemons => Set<PokemonEntity>();
    //public DbSet<AbilityEntity> Abilities => Set<AbilityEntity>();
    //public DbSet<MoveEntity> Moves => Set<MoveEntity>();
    //public DbSet<TypeEntity> Types => Set<TypeEntity>();
    //public DbSet<SpriteEntity> Sprites => Set<SpriteEntity>();
    //public DbSet<PokemonDetailEntity> Details => Set<PokemonDetailEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    /// <summary>
    /// Only used to create migrations and update local database
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);

            if (AppDomain.CurrentDomain.FriendlyName.Contains("testhost"))
                optionsBuilder.UseInMemoryDatabase("ApplicationDb");
            else
            {
                var solutionPath = GetSolutionPath();

                var connectionString = string.Empty;
                if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DefaultConnection")))
                {
                    Console.WriteLine($"Using appsettings.json to get connection string");
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(solutionPath.FullName)
                        .AddJsonFile("src/WebApi/appsettings.json", optional: false)
                        .Build();

                    Environment.SetEnvironmentVariable("DefaultConnection", builder.GetConnectionString("DefaultConnection"));
                    connectionString = builder.GetConnectionString("DefaultConnection");
                }
                else
                {
                    Console.WriteLine($"Using Environment Variable to get connection string");
                    connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                    Environment.SetEnvironmentVariable("DefaultConnection", connectionString);

                    Console.WriteLine($"Connection string: {connectionString}");
                }

                Console.WriteLine($"Connection string to be used: {connectionString}");
                optionsBuilder.UseSqlServer(connectionString,
                    builder => builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
            }

            base.OnConfiguring(optionsBuilder);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        //_mediator.DispatchDomainEvents(this).Wait();

        return base.SaveChanges();
    }

    private static DirectoryInfo GetSolutionPath(string currentPath = null!)
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
