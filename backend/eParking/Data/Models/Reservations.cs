using eParking.Helper.BaseClasses;
using System;

namespace eParking.Data.Models
{
    public class Reservations : BaseEntity
    {
        public int CarID { get; set; }
        public int ParkingSpotID { get; set; }
        public int ReservationTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double FinalPrice { get; set; }
    }
}