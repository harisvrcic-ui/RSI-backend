using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ColorsEndpoints.ColorsGetAllEndpoint;

namespace eParking.Endpoints.ColorsEndpoints;

[Route("Colors")]
public class ColorsGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ColorsGetAllRequest>
    .WithResult<MyPagedList<ColorsGetAllResponse>>
{
    [HttpGet("filter")]
    public override async Task<MyPagedList<ColorsGetAllResponse>> HandleAsync([FromQuery] ColorsGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = db.Colors
            .AsQueryable();

        // Filter by search query
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            query = query.Where(c => c.Name.Contains(request.Q));
        }

        // Project to result type
        var projectedQuery = query.Select(c => new ColorsGetAllResponse
        {
            ID = c.ID,
            Name = c.Name,
            HexCode = c.HexCode
        });

        // Create paginated response with filter
        var result = await MyPagedList<ColorsGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class ColorsGetAllRequest : MyPagedRequest
    {
        public string? Q { get; set; } = string.Empty;
    }

    public class ColorsGetAllResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HexCode { get; set; } = string.Empty;
    }
}

