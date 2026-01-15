using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CountryEndpoints.CountryGetAllEndpoint;
using static eParking.Endpoints.ParkingSpotTypeEndpoints.ParkingSpotTypesGetAllEndpoint;

namespace eParking.Endpoints.ParkingSpotTypeEndpoints;

[Route("ParkingSpotTypes")]
public class ParkingSpotTypesGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ParkingSpotTypesGetAllRequest>
    .WithResult<MyPagedList<ParkingSpotTypesGetAllResponse>>
{
    [HttpGet("filter")]
    public override async Task<MyPagedList<ParkingSpotTypesGetAllResponse>> HandleAsync([FromQuery] ParkingSpotTypesGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = db.ParkingSpotTypes
            .AsQueryable();

        // Filter by search query
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            query = query.Where(c => c.Name.Contains(request.Q));
        }

        // Project to result type
        var projectedQuery = query.Select(c => new ParkingSpotTypesGetAllResponse
        {
            ID = c.ID,
            Name = c.Name,
            Description = c.Description,
            PriceMultiplier = c.PriceMultiplier,
        });

        // Create paginated response with filter
        var result = await MyPagedList<ParkingSpotTypesGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class ParkingSpotTypesGetAllRequest : MyPagedRequest
    {
        public string? Q { get; set; } = string.Empty;
    }

    public class ParkingSpotTypesGetAllResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public int PriceMultiplier { get; set; }
    }
}

