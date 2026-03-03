using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CarsEndpoints.CarsDeleteEndpoint;

namespace eParking.Endpoints.CarsEndpoints;

[Route("Cars")]
public class CarsDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult
{
    [HttpDelete("{id}")]
    public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var car = await db.Cars.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (car == null)
        {
            return NotFound(new { message = "Car not found" });
        }

        // Obriši sve rezervacije ovog auta pa onda sam auto (cascade)
        var reservationsToRemove = await db.Reservations.Where(r => r.CarID == id).ToListAsync(cancellationToken);
        db.Reservations.RemoveRange(reservationsToRemove);

        car.IsActive = false;
        db.Cars.Remove(car);
        await db.SaveChangesAsync(cancellationToken);
        return Ok();
    }
}

