using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CountryEndpoints.CountryGetAllEndpoint;
using static eParking.Endpoints.ParkingZonesEndpoints.ParkingZonesGetAllEndpoint;

namespace eParking.Endpoints.ParkingZonesEndpoints;

[Route(ApiRouteConstants.ParkingZones)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ParkingZonesGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ParkingZonesGetAllRequest>
    .WithResult<MyPagedList<ParkingZonesGetAllResponse>>
{
    [HttpGet(ApiRouteConstants.Filter)]
    public override async Task<MyPagedList<ParkingZonesGetAllResponse>> HandleAsync([FromQuery] ParkingZonesGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = db.ParkingZones
            .AsQueryable();

        // Filter by search query
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            query = query.Where(c => c.Name != null && c.Name.Contains(q));
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
        [FromQuery(Name = "q")]
        public string? Q { get; set; } = string.Empty;
    }

    public class ParkingZonesGetAllResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

    }
}

