using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReservationTypeEndpoints.ReservationTypeDeleteEndpoint;

namespace eParking.Endpoints.ReservationTypeEndpoints;

[Route(ApiRouteConstants.ReservationTypes)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ReservationTypeDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete(ApiRouteConstants.Id)]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var ReservationType = await db.ReservationTypes.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (ReservationType == null)
        {
            throw new KeyNotFoundException("ReservationType not found");
        }

        db.ReservationTypes.Remove(ReservationType);


        await db.SaveChangesAsync(cancellationToken);
    }


}

