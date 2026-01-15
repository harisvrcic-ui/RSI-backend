using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ParkingZonesEndpoints.ParkingZonesGetByIdEndpoint;

namespace eParking.Endpoints.ParkingZonesEndpoints;

[Route("ParkingZones")]
public class ParkingZonesGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<ParkingZonesGetByIdResponse>
{
    [HttpGet("{id}")]
    public override async Task<ActionResult<ParkingZonesGetByIdResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var ParkingZone = await db.ParkingZones
            .Where(c => c.ID == id)
            .Select(c => new ParkingZonesGetByIdResponse
            {
                ID = c.ID,
                Name = c.Name,
                IsActive = c.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (ParkingZone == null)
        {
            return NotFound("Parking zone not found");
        }

        return Ok(ParkingZone);
    }

    public class ParkingZonesGetByIdResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}

