namespace AuctionService.IntegrationTests;

[Collection("Shared collection")]
public class AuctionControllerTests: IAsyncLifetime
{
    private readonly CustomWebAppFactory _factory;
    private readonly HttpClient _httpClient;
    private const string GtId = "afbee524-5972-4075-8800-7d1f9d7b0a0c";

    public AuctionControllerTests(CustomWebAppFactory factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task GetActions_ShouldReturn3Auctions()
    {
        // the arrange phase has been done by the custom web app factory
        
        // act
        var response = await _httpClient.GetFromJsonAsync<List<AuctionDto>>("api/Auctions");

        //assert
        Assert.NotNull(response);
        Assert.Equal(3, response.Count);
    }
    
    [Fact]
    public async Task GetActionById_WithValidId_ShouldReturnAuction()
    {
        // the arrange phase has been done by the custom web app factory
        
        // act
        var response = await _httpClient.GetFromJsonAsync<AuctionDto>($"api/auctions/{GtId}");

        //assert
        Assert.NotNull(response);
        Assert.Equal("GT", response.Model);
    }
    
    [Fact]
    public async Task GetActionById_WithInvalidId_ShouldReturn404()
    {
        // the arrange phase has been done by the custom web app factory
        
        // act
        var response = await _httpClient.GetAsync($"api/auctions/{Guid.NewGuid()}");

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task CreateAuction_WithNoAuth_ShouldReturn401()
    {
        // the arrange phase has been done by the custom web app factory
        var auction = new CreateAuctionDto { Make = "test" };
        // act
        var response = await _httpClient.PostAsJsonAsync($"api/auctions", auction);

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task CreateAuction_WithAuth_ShouldReturn201()
    {
        // the arrange phase has been done by the custom web app factory
        var auction = GetAuctionForCreate();
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));
        
        // act
        var response = await _httpClient.PostAsJsonAsync($"api/auctions", auction);

        //assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var createdAuction = await response.Content.ReadFromJsonAsync<AuctionDto>();
        Assert.Equal("bob", createdAuction.Seller);
    }
    
    [Fact]
    public async Task CreateAuction_WithInvalidCreateAuctionDto_ShouldReturn400()
    {
        // arrange
        // the arrange phase has been done by the custom web app factory
        var auction = GetAuctionForCreate();
        auction.Make = null;
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));
        
        // act
        var response = await _httpClient.PostAsJsonAsync($"api/auctions", auction);
        
        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAuction_WithValidUpdateDtoAndUser_ShouldReturn200()
    {
        // arrange
        var updateAuction = new UpdateAuctionDto { Make = "Updated"};
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));
        
        // act
        var response = await _httpClient.PutAsJsonAsync($"api/auctions/{GtId}", updateAuction);
        
        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAuction_WithValidUpdateDtoAndInvalidUser_ShouldReturn403()
    {
        // arrange
        var updateAuction = new UpdateAuctionDto { Make = "Updated"};
        _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("notbob"));
        
        // act
        var response = await _httpClient.PutAsJsonAsync($"api/auctions/{GtId}", updateAuction);
        
        // assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
    
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();
        DbHelper.ReinitDbForTests(db);
        
        return Task.CompletedTask;
    }

    private static CreateAuctionDto GetAuctionForCreate()
    {
        return new CreateAuctionDto
        {
            Make = "test",
            Model = "testModel",
            ImageUrl = "test",
            Color = "test",
            Mileage = 15,
            Year = 2017,
            ReservePrice = 100
        };
    }
}