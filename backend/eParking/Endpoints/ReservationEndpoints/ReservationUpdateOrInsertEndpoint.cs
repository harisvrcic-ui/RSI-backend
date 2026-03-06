using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReservationEndpoints.ReservationUpdateOrInsertEndpoint;

namespace eParking.Endpoints.ReservationEndpoints;

[Route(ApiRouteConstants.Reservations)]
[MyAuthorization(isAdmin: true, isUser: true)]
public class ReservationUpdateOrInsertEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ReservationUpdateOrInsertRequest>
    .WithoutResult
{
    [HttpPost]
    public override async Task HandleAsync([FromBody] ReservationUpdateOrInsertRequest request, CancellationToken cancellationToken = default)
    {
        bool isInsert = request.ID == null || request.ID == 0;
        Reservations? Reservation;

        if (isInsert)
        {
            Reservation = new Reservations();
            db.Reservations.Add(Reservation);
        }
        else
        {
            Reservation = await db.Reservations.SingleOrDefaultAsync(x => x.ID == request.ID, cancellationToken);

            if (Reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found");
            }
        }

        Reservation.CarID = request.CarID;
        Reservation.ParkingSpotID = request.ParkingSpotID;
        Reservation.ReservationTypeID = request.ReservationTypeID;
        Reservation.StartDate = request.StartDate;
        Reservation.EndDate = request.EndDate;
        Reservation.FinalPrice = request.FinalPrice;


        await db.SaveChangesAsync(cancellationToken);
    }

    public class ReservationUpdateOrInsertRequest
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

