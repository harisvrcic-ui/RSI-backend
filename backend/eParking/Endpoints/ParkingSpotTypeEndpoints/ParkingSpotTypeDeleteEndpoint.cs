using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ParkingSpotTypeEndpoints.ParkingSpotTypeDeleteEndpoint;

namespace eParking.Endpoints.ParkingSpotTypeEndpoints;

[Route(ApiRouteConstants.ParkingSpotTypes)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ParkingSpotTypeDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete(ApiRouteConstants.Id)]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var parkingSpotType = await db.ParkingSpotTypes.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (parkingSpotType == null)
        {
            throw new KeyNotFoundException("Parking Spot Type not found");
        }

        db.ParkingSpotTypes.Remove(parkingSpotType);
      

        await db.SaveChangesAsync(cancellationToken);
    }


}

