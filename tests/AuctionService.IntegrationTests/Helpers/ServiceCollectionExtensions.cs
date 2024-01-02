namespace AuctionService.IntegrationTests.Helpers;

public static class ServiceCollectionExtensions
{
    public static void RemoveDbContext<T>(this IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d =>
            d.ServiceType == typeof(DbContextOptions<AuctionDbContext>));
            
        if (descriptor != null) services.Remove(descriptor);
    }

    public static void EnsureCreated<T>(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var scopedService = scope.ServiceProvider;
        var db = scopedService.GetRequiredService<AuctionDbContext>();
            
        db.Database.Migrate(); 
        DbHelper.InitDbForTests(db);
    }
}