using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReservationEndpoints.ReservationGetByIdEndpoint;

namespace eParking.Endpoints.ReservationEndpoints;

[Route(ApiRouteConstants.Reservations)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ReservationGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<ReservationGetByIdResponse>
{
    [HttpGet(ApiRouteConstants.Id)]
    public override async Task<ActionResult<ReservationGetByIdResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var Reservation = await db.Reservations
            .Where(c => c.ID == id)
            .Select(c => new ReservationGetByIdResponse
            {
                ID = c.ID,
                CarID = c.CarID,
                ParkingSpotID = c.ParkingSpotID,
                ReservationTypeID = c.ReservationTypeID,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                FinalPrice = c.FinalPrice
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (Reservation == null)
        {
            return NotFound("Reservation not found");
        }

        return Ok(Reservation);
    }

    public class ReservationGetByIdResponse
    {
        public int ID { get; set; }
        public int CarID { get; set; }
        public int ParkingSpotID { get; set; }
        public int ReservationTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double FinalPrice { get; set; }
    }
}

