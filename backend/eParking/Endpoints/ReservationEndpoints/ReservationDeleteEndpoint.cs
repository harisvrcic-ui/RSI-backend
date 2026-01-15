using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReservationEndpoints.ReservationDeleteEndpoint;

namespace eParking.Endpoints.ReservationEndpoints;

[Route("Reservations")]
public class ReservationDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete("{id}")]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var Reservation = await db.Reservations.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (Reservation == null)
        {
            throw new KeyNotFoundException("Reservation not found");
        }

        db.Reservations.Remove(Reservation);


        await db.SaveChangesAsync(cancellationToken);
    }


}

