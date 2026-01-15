using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ParkingSpotTypeEndpoints.ParkingSpotTypeGetByIdEndpoint;

namespace eParking.Endpoints.ParkingSpotTypeEndpoints;

[Route("ParkingSpotTypes")]
public class ParkingSpotTypeGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<ParkingSpotTypeGetByIdResponse>
{
    [HttpGet("{id}")]
    public override async Task<ActionResult<ParkingSpotTypeGetByIdResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var ParkingSpotType = await db.ParkingSpotTypes
            .Where(c => c.ID == id)
            .Select(c => new ParkingSpotTypeGetByIdResponse
            {
                ID = c.ID,
                Name = c.Name,
                Description = c.Description,
                PriceMultiplier = c.PriceMultiplier,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (ParkingSpotType == null)
        {
            return NotFound("Parking spot type not found");
        }

        return Ok(ParkingSpotType);
    }

    public class ParkingSpotTypeGetByIdResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public int PriceMultiplier { get; set; }
    }
}

