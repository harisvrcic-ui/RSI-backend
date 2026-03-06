using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ColorsEndpoints.ColorsDeleteEndpoint;

namespace eParking.Endpoints.ColorsEndpoints;

[Route(ApiRouteConstants.Colors)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ColorsDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete(ApiRouteConstants.Id)]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var Color = await db.Colors.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (Color == null)
        {
            throw new KeyNotFoundException("Color not found");
        }


        db.Colors.Remove(Color);


        await db.SaveChangesAsync(cancellationToken);
    }


}

