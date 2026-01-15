using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReviewsEndpoints.ReviewsGetAllEndpoint;

namespace eParking.Endpoints.ReviewsEndpoints;

[Route("Reviews")]
public class ReviewsGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ReviewsGetAllRequest>
    .WithResult<MyPagedList<ReviewsGetAllResponse>>
{
    [HttpGet("filter")]
    public override async Task<MyPagedList<ReviewsGetAllResponse>> HandleAsync([FromQuery] ReviewsGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = db.Reviews
            .AsQueryable();

        // Filter by search query
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            //query = query.Where(c => c.Name.Contains(request.Q));
        }

        // Project to result type
        var projectedQuery = query.Select(c => new ReviewsGetAllResponse
        {
            ID = c.ID,
            UserId = c.UserId,
            ReservationId = c.ReservationId,
            Rating = c.Rating,
            Comment = c.Comment
        });

        // Create paginated response with filter
        var result = await MyPagedList<ReviewsGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class ReviewsGetAllRequest : MyPagedRequest
    {
        public string? Q { get; set; } = string.Empty;
    }

    public class ReviewsGetAllResponse
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int ReservationId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}

