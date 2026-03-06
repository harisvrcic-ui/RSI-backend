using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CityEndpoints.CityGetAllEndpoint;

namespace eParking.Endpoints.CityEndpoints;

[Route(ApiRouteConstants.Cities)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class CityGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<CityGetAllRequest>
    .WithResult<MyPagedList<CityGetAllResponse>>
{
    [HttpGet(ApiRouteConstants.Filter)]
    public override async Task<MyPagedList<CityGetAllResponse>> HandleAsync([FromQuery] CityGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = db.Cities
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Q))
            query = query.Where(c => c.Name.Contains(request.Q));
        if (request.CountryId.HasValue && request.CountryId.Value > 0)
            query = query.Where(c => c.CountryId == request.CountryId.Value);
        if (request.IsActive.HasValue)
            query = query.Where(c => c.IsActive == request.IsActive.Value);

        var projectedQuery = query.Select(c => new CityGetAllResponse
        {
            ID = c.ID,
            Name = c.Name,
            CountryId = c.CountryId,
            CountryName = c.Country.Name
        });

        // Create paginated response with filter
        var result = await MyPagedList<CityGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class CityGetAllRequest : MyPagedRequest
    {
        public string? Q { get; set; } = string.Empty;
        public int? CountryId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CityGetAllResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public string CountryName { get; set; } = string.Empty;
    }
} 