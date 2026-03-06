using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReviewsEndpoints.ReviewsUpdateOrInsertEndpoint;

namespace eParking.Endpoints.ReviewsEndpoints;

[Route(ApiRouteConstants.Reviews)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ReviewsUpdateOrInsertEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ReviewsUpdateOrInsertRequest>
    .WithoutResult
{
    [HttpPost]
    public override async Task HandleAsync([FromBody] ReviewsUpdateOrInsertRequest request, CancellationToken cancellationToken = default)
    {
        bool isInsert = request.ID == null || request.ID == 0;
        Reviews? Review;

        if (isInsert)
        {
            Review = new Reviews();
            db.Reviews.Add(Review);
        }
        else
        {
            Review = await db.Reviews.SingleOrDefaultAsync(x => x.ID == request.ID, cancellationToken);

            if (Review == null)
            {
                throw new KeyNotFoundException("Review not found");
            }
        }

        Review.UserId = request.UserId;
        Review.ReservationId = request.ReservationId;
        Review.Rating = request.Rating;
        Review.Comment = request.Comment;


        await db.SaveChangesAsync(cancellationToken);
    }

    public class ReviewsUpdateOrInsertRequest
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int ReservationId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}

