using Microsoft.EntityFrameworkCore;

namespace AccountService.Data;

public static class AutoMigrations
{
    public static WebApplication UseAutoMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        try
        {
            var migrations = databaseContext.Database.GetPendingMigrations();

            if (migrations.Any())
            {
                databaseContext.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            Console.WriteLine($"An error occurred during migrations: {ex.Message}");
        }

        return app;
    }
}
