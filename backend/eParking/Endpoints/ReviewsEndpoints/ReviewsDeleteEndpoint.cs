using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReviewsEndpoints.ReviewsDeleteEndpoint;

namespace eParking.Endpoints.ReviewsEndpoints;

[Route(ApiRouteConstants.Reviews)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ReviewsDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete(ApiRouteConstants.Id)]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var Review = await db.Reviews.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (Review == null)
        {
            throw new KeyNotFoundException("Review not found");
        }

        db.Reviews.Remove(Review);


        await db.SaveChangesAsync(cancellationToken);
    }


}

