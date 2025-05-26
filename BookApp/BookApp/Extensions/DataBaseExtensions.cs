using BookApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Extensions;

public static class DataBaseExtensions
{
    public static async Task<IHost> ApplyMigrationsAsync(this IHost webHost)
    {
        using (var scope = webHost.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var env = services.GetRequiredService<IWebHostEnvironment>();
            var context = services.GetRequiredService<AppDbContext>();
            try
            {
                var arePendingMigrations = context.Database.GetPendingMigrations().Any();
                await context.Database.MigrateAsync();
                if (arePendingMigrations)
                {
                    await context.SeedDatabaseIfNoBooksAsync(env.WebRootPath);  
                }
            }
            
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while creating/migrating or seeding the database.");

                throw;
            }
        }

        return webHost;

    }
}