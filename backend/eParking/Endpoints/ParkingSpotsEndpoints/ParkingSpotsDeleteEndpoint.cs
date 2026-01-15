using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ParkingSpotsEndpoints.ParkingSpotsDeleteEndpoint;

namespace eParking.Endpoints.ParkingSpotsEndpoints;

[Route("ParkingSpots")]
public class ParkingSpotsDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete("{id}")]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var parkingSpot = await db.ParkingSpots.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (parkingSpot == null)
        {
            throw new KeyNotFoundException("Parking Spot not found");
        }

        parkingSpot.IsActive = false;


        await db.SaveChangesAsync(cancellationToken);
    }


}

