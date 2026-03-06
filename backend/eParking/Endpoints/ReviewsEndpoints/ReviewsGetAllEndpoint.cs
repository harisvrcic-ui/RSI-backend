using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReviewsEndpoints.ReviewsGetAllEndpoint;

namespace eParking.Endpoints.ReviewsEndpoints;

[Route(ApiRouteConstants.Reviews)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ReviewsGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ReviewsGetAllRequest>
    .WithResult<MyPagedList<ReviewsGetAllResponse>>
{
    [HttpGet(ApiRouteConstants.Filter)]
    public override async Task<MyPagedList<ReviewsGetAllResponse>> HandleAsync([FromQuery] ReviewsGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = db.Reviews
            .AsQueryable();

        var q = request.Q?.Trim();
        if (!string.IsNullOrWhiteSpace(q))
        {
            if (int.TryParse(q, out var idVal))
                query = query.Where(r => r.ID == idVal);
            else
                query = query.Where(r => r.Comment != null && r.Comment.Contains(q));
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
        [FromQuery(Name = "q")]
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

