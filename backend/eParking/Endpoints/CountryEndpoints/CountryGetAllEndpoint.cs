using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CountryEndpoints.CountryGetAllEndpoint;

namespace eParking.Endpoints.CountryEndpoints;

[Route(ApiRouteConstants.Countries)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class CountryGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<CountryGetAllRequest>
    .WithResult<MyPagedList<CountryGetAllResponse>>
{
    [HttpGet(ApiRouteConstants.Filter)]
    public override async Task<MyPagedList<CountryGetAllResponse>> HandleAsync([FromQuery] CountryGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = db.Countries
            .AsQueryable();

        // Filter by search query
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            query = query.Where(c => c.Name.Contains(request.Q));
        }

        // Project to result type
        var projectedQuery = query.Select(c => new CountryGetAllResponse
        {
            ID = c.ID,
            Name = c.Name
        });

        // Create paginated response with filter
        var result = await MyPagedList<CountryGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class CountryGetAllRequest : MyPagedRequest
    {
        public string? Q { get; set; } = string.Empty;
    }

    public class CountryGetAllResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

