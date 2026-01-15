using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReservationTypeEndpoints.ReservationTypeGetAllEndpoint;

namespace eParking.Endpoints.ReservationTypeEndpoints;

[Route("ReservationTypes")]
public class ReservationTypeGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ReservationTypeGetAllRequest>
    .WithResult<MyPagedList<ReservationTypeGetAllResponse>>
{
    [HttpGet("filter")]
    public override async Task<MyPagedList<ReservationTypeGetAllResponse>> HandleAsync([FromQuery] ReservationTypeGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = db.ReservationTypes
            .AsQueryable();

        // Filter by search query
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            query = query.Where(c => c.Name.Contains(request.Q));
        }

        // Project to result type
        var projectedQuery = query.Select(c => new ReservationTypeGetAllResponse
        {
            ID = c.ID,
            Name = c.Name,
            Price = c.Price
        });

        // Create paginated response with filter
        var result = await MyPagedList<ReservationTypeGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class ReservationTypeGetAllRequest : MyPagedRequest
    {
        public string? Q { get; set; } = string.Empty;
    }

    public class ReservationTypeGetAllResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }

    }
}

