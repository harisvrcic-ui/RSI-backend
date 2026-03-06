using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eParking.Endpoints.GenderEndpoints;

[Route(ApiRouteConstants.Genders)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class GenderDeleteEndpoint(ApplicationDbContext db, IMyDistributedCacheService cache) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete(ApiRouteConstants.Id)]
    public override async Task HandleAsync([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var gender = await db.Set<Gender>()
            .FirstOrDefaultAsync(g => g.ID == id, cancellationToken);

        if (gender == null)
        {
            throw new Exception($"Gender with ID {id} not found.");
        }

        gender.IsActive = false;

        db.Genders.Update(gender);

        await db.SaveChangesAsync(cancellationToken);

        await cache.InvalidateGendersAsync(cancellationToken);
    }
}
