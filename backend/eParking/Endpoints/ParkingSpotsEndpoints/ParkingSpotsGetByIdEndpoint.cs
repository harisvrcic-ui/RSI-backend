using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ParkingSpotsEndpoints.ParkingSpotsGetByIdEndpoint;

namespace eParking.Endpoints.ParkingSpotsEndpoints;

[Route("ParkingSpots")]
public class ParkingSpotsGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<ParkingSpotsGetByIdResponse>
{
    [HttpGet("{id}")]
    public override async Task<ActionResult<ParkingSpotsGetByIdResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var ParkingSpot = await db.ParkingSpots
            .Where(c => c.ID == id)
            .Select(c => new ParkingSpotsGetByIdResponse
            {
                ID = c.ID,
                ParkingNumber = c.ParkingNumber,
                ParkingSpotTypeId = c.ParkingSpotTypeId,
                ZoneId = c.ZoneId,
                IsActive = c.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (ParkingSpot == null)
        {
            return NotFound("Parking spot not found");
        }

        return Ok(ParkingSpot);
    }

    public class ParkingSpotsGetByIdResponse
    {
        public int ID { get; set; }
        public int ParkingNumber { get; set; }
        public int ParkingSpotTypeId { get; set; }
        public int ZoneId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

