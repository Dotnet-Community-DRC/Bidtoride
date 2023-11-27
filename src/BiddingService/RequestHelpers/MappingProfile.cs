namespace BiddingService.RequestHelpers;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Bid, BidDto>();
        CreateMap<Bid, BidPlaced>();
    }
}