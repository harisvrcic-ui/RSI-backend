using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.BrandEndpoints.BrandGetAllEndpoint;


namespace eParking.Endpoints.BrandEndpoints;

[Route("brands")]
public class BrandGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<BrandGetAllRequest>
    .WithResult<MyPagedList<BrandGetAllResponse>>
{
    [HttpGet("filter")]
    public override async Task<MyPagedList<BrandGetAllResponse>> HandleAsync([FromQuery] BrandGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = db.Brands
            .AsQueryable();

        // Filter by search query
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            query = query.Where(c => c.Name.Contains(request.Q));
        }

        // Project to result type
        var projectedQuery = query.Select(c => new BrandGetAllResponse
        {
            ID = c.ID,
            Name = c.Name,
            Logo = c.Logo,
            IsActive = c.IsActive,
        });

        // Create paginated response with filter
        var result = await MyPagedList<BrandGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class BrandGetAllRequest : MyPagedRequest
    {
        public string? Q { get; set; } = string.Empty;
    }

    public class BrandGetAllResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? Logo { get; set; }
        public bool IsActive { get; set; }
    }
}

