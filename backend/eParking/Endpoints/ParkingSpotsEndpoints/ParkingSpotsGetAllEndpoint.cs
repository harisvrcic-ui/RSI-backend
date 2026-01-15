using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CountryEndpoints.CountryGetAllEndpoint;
using static eParking.Endpoints.ParkingSpotsEndpoints.ParkingSpotsGetAllEndpoint;

namespace eParking.Endpoints.ParkingSpotsEndpoints;

[Route("ParkingSpots")]
public class ParkingSpotsGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ParkingSpotsGetAllRequest>
    .WithResult<MyPagedList<ParkingSpotsGetAllResponse>>
{
    [HttpGet("filter")]
    public override async Task<MyPagedList<ParkingSpotsGetAllResponse>> HandleAsync([FromQuery] ParkingSpotsGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = db.ParkingSpots
            .AsQueryable();

        // Filter by search query
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            //query = query.Where(c => c.Name.Contains(request.Q));
        }

        // Project to result type
        var projectedQuery = query.Select(c => new ParkingSpotsGetAllResponse
        {
            ID = c.ID,
            ParkingNumber = c.ParkingNumber,
            ParkingSpotTypeId = c.ParkingSpotTypeId,
            ZoneId = c.ZoneId,
            IsActive = c.IsActive
        });

        // Create paginated response with filter
        var result = await MyPagedList<ParkingSpotsGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class ParkingSpotsGetAllRequest : MyPagedRequest
    {
        public string? Q { get; set; } = string.Empty;
    }

    public class ParkingSpotsGetAllResponse
    {
        public int ID { get; set; }
        public int ParkingNumber { get; set; }
        public int ParkingSpotTypeId { get; set; }
        public int ZoneId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

