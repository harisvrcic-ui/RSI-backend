using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ParkingSpotTypeEndpoints.ParkingSpotTypeDeleteEndpoint;

namespace eParking.Endpoints.ParkingSpotTypeEndpoints;

[Route("ParkingSpotTypes")]
public class ParkingSpotTypeDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete("{id}")]
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

