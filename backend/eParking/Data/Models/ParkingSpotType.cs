using eParking.Helper.BaseClasses;
using System;

namespace eParking.Data.Models
{
    public class ParkingSpotType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PriceMultiplier { get; set; }

    }
}
