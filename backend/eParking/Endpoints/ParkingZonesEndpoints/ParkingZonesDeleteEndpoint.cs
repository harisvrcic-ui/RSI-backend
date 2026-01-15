using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ParkingZonesEndpoints.ParkingZonesDeleteEndpoint;

namespace eParking.Endpoints.ParkingZonesEndpoints;

[Route("ParkingZones")]
public class ParkingZonesDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete("{id}")]
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

