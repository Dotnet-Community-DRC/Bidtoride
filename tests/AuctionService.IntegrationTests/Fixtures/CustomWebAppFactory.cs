using WebMotions.Fake.Authentication.JwtBearer;

namespace AuctionService.IntegrationTests.Fixtures;

public class CustomWebAppFactory: WebApplicationFactory<Program>, IAsyncLifetime
{
    private PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().Build(); 
    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<AuctionDbContext>();

            services.AddDbContext<AuctionDbContext>(options =>
            {
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString());
            });

            services.AddMassTransitTestHarness();

            services.EnsureCreated<AuctionDbContext>();

            services.AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme)
                .AddFakeJwtBearer(opt =>
                {
                    opt.BearerValueType = FakeJwtBearerBearerValueType.Jwt;
                });
        });
    }

    public async Task DisposeAsync() => await _postgreSqlContainer.DisposeAsync().AsTask();
}