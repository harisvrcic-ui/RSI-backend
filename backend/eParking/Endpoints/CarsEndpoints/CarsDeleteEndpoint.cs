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
<<<<<<< HEAD
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
=======
    .WithoutResult
{
    [HttpDelete("{id}")]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var Car = await db.Cars.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (Car == null)
        {
            throw new KeyNotFoundException("Car not found");
        }

        Car.IsActive = false;


        await db.SaveChangesAsync(cancellationToken);
    }


>>>>>>> 9d8f07312ad0d0046110f2fb150f74fa5ef7b7f9
}

