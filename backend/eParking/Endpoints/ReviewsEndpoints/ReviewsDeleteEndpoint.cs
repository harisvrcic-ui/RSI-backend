using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReviewsEndpoints.ReviewsDeleteEndpoint;

namespace eParking.Endpoints.ReviewsEndpoints;

[Route("Reviews")]
public class ReviewsDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete("{id}")]
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

