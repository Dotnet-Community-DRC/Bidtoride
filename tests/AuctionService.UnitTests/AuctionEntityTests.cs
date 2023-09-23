using AuctionService.Entities;

namespace AuctionService.UnitTests;

public class AuctionEntityTests
{
    [Fact]
    public void HasReservePrice_ReservePriceGreatZero_True()
    {
        // Arrange
        var auction = new Auction { Id = Guid.NewGuid(), ReservePrice = 10 };
        
        // Act
        var result = auction.HasReservePrice();
        
        // assert
        Assert.True(result);
    }
}