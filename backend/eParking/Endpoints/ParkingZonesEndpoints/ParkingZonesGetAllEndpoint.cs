using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CountryEndpoints.CountryGetAllEndpoint;
using static eParking.Endpoints.ParkingZonesEndpoints.ParkingZonesGetAllEndpoint;

namespace eParking.Endpoints.ParkingZonesEndpoints;

[Route("ParkingZones")]
public class ParkingZonesGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ParkingZonesGetAllRequest>
    .WithResult<MyPagedList<ParkingZonesGetAllResponse>>
{
    [HttpGet("filter")]
    public override async Task<MyPagedList<ParkingZonesGetAllResponse>> HandleAsync([FromQuery] ParkingZonesGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = db.ParkingZones
            .AsQueryable();

        // Filter by search query
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            //query = query.Where(c => c.Name.Contains(request.Q));
        }

        // Project to result type
        var projectedQuery = query.Select(c => new ParkingZonesGetAllResponse
        {
            ID = c.ID,
            Name = c.Name,
            IsActive = c.IsActive
        });

        // Create paginated response with filter
        var result = await MyPagedList<ParkingZonesGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class ParkingZonesGetAllRequest : MyPagedRequest
    {
        public string? Q { get; set; } = string.Empty;
    }

    public class ParkingZonesGetAllResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

    }
}

