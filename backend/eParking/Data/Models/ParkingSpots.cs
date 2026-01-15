using eParking.Helper.BaseClasses;
using System;

namespace eParking.Data.Models
{
    public class ParkingSpots : BaseEntity
    {
        public int ParkingNumber { get; set; }
        public int ParkingSpotTypeId { get; set; }
        public int ZoneId { get; set; }
        public bool IsActive { get; set; } = true;

    }
}