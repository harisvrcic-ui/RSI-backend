using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ParkingZonesEndpoints.ParkingZonesDeleteEndpoint;

namespace eParking.Endpoints.ParkingZonesEndpoints;

[Route(ApiRouteConstants.ParkingZones)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ParkingZonesDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete(ApiRouteConstants.Id)]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var ParkingZone = await db.ParkingZones.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (ParkingZone == null)
        {
            throw new KeyNotFoundException("Parking Spot not found");
        }

        ParkingZone.IsActive = false;


        await db.SaveChangesAsync(cancellationToken);
    }


}

