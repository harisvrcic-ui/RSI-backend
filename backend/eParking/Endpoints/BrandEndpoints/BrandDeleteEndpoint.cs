using eParking.Data;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.BrandEndpoints.BrandDeleteEndpoint;

namespace eParking.Endpoints.BrandEndpoints;

[Route("brands")]
public class BrandDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete("{id}")]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var brand = await db.Brands.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (brand == null)
        {
            throw new KeyNotFoundException("Brand not found");
        }


        brand.IsActive = false; // soft delete

        await db.SaveChangesAsync(cancellationToken);
    }


}

