using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReviewsEndpoints.ReviewsGetByIdEndpoint;

namespace eParking.Endpoints.ReviewsEndpoints;

[Route(ApiRouteConstants.Reviews)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ReviewsGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<ReviewsGetByIdResponse>
{
    [HttpGet(ApiRouteConstants.Id)]
    public override async Task<ActionResult<ReviewsGetByIdResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var Review = await db.Reviews
            .Where(c => c.ID == id)
            .Select(c => new ReviewsGetByIdResponse
            {
                ID = c.ID,
                UserId = c.UserId,
                ReservationId = c.ReservationId,
                Rating = c.Rating,
                Comment = c.Comment

            })
            .FirstOrDefaultAsync(cancellationToken);

        if (Review == null)
        {
            return NotFound("Review not found");
        }

        return Ok(Review);
    }

    public class ReviewsGetByIdResponse
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int ReservationId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}

