using AccountService.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace AccountService.Tests;

public class EndpointFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    public DatabaseContext DatabaseContext { get; set; } = default!;

    private readonly PostgreSqlContainer _testcontainerDatabase =
        new PostgreSqlBuilder()
            .WithDatabase("postgres")
            .WithUsername("pguser")
            .WithPassword("pgpassword")
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // use separate configs and secrets for testing
        //builder.UseEnvironment("Testing")
        //    .UseConfiguration(new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.Testing.json")
        //        .Build());

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<DatabaseContext>>();
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(_testcontainerDatabase.GetConnectionString());
            });

            using var scope = services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var migrations = context.Database.GetPendingMigrations();
            if (migrations.Any())
            {
                context.Database.Migrate();
            }
        });
    }

    public async Task InitializeAsync()
    {
        await _testcontainerDatabase.StartAsync();
        var scope = Services.CreateScope();
        var databaseContext =
            scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        // add initial data here
        DatabaseContext = databaseContext;
    }

    public new async Task DisposeAsync()
    {
        await _testcontainerDatabase.DisposeAsync();
    }
}
