using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CarsEndpoints.CarsDeleteEndpoint;

namespace eParking.Endpoints.CarsEndpoints;

[Route(ApiRouteConstants.Cars)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class CarsDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult
{
    [HttpDelete(ApiRouteConstants.Id)]
    public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var car = await db.Cars.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (car == null)
        {
            return NotFound(new { message = "Car not found" });
        }

        // Delete all reservations for this car then the car itself (cascade)
        var reservationsToRemove = await db.Reservations.Where(r => r.CarID == id).ToListAsync(cancellationToken);
        db.Reservations.RemoveRange(reservationsToRemove);

        car.IsActive = false;
        db.Cars.Remove(car);
        await db.SaveChangesAsync(cancellationToken);
        return Ok();
    }
}
