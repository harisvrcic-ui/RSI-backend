using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eParking.Endpoints.CityEndpoints;

[Route(ApiRouteConstants.Cities)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class CityDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult
{
    [HttpDelete(ApiRouteConstants.Id)]
    public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var city = await db.Cities.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (city == null)
            return NotFound("City not found");

        city.IsActive = false;
        db.Cities.Remove(city);
        await db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
} 