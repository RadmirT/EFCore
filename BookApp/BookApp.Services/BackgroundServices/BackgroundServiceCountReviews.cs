namespace BookApp.Services.BackgroundServices;

using BookApp.Entities;
using BookApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class BackgroundServiceCountReviews : BackgroundService
{
    private static TimeSpan _period = new(0, 1, 0, 0);

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BackgroundServiceCountReviews> _logger;

    public BackgroundServiceCountReviews(IServiceScopeFactory scopeFactory, ILogger<BackgroundServiceCountReviews> logger, TimeSpan periodOverride = default)
    {
        this._scopeFactory = scopeFactory;
        this._logger = logger;

        if (periodOverride != TimeSpan.Zero)
        {
            _period = periodOverride;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await this.DoWorkAsync(stoppingToken);
            await Task.Delay(_period, stoppingToken);
        }
    }
    
    private async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        using var scope = this._scopeFactory.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var numReviews = await context.Set<Review>().CountAsync(stoppingToken);
        this._logger.LogInformation( "Number of reviews: {numReviews}", numReviews);
    }
}