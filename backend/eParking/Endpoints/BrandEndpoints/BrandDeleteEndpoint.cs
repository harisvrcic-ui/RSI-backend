using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.BrandEndpoints.BrandDeleteEndpoint;

namespace eParking.Endpoints.BrandEndpoints;

[Route(ApiRouteConstants.Brands)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class BrandDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete(ApiRouteConstants.Id)]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var brand = await db.Brands.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (brand == null)
        {
            throw new KeyNotFoundException("Brand not found");
        }


        brand.IsActive = false; // soft delete
        db.Brands.Remove(brand);
        await db.SaveChangesAsync(cancellationToken);
    }


}

