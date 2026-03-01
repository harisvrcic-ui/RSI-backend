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
        /// <summary>Naziv lokacije za pretragu i prikaz (npr. Aria mall, Vijećnica, Baščaršija).</summary>
        public string? DisplayName { get; set; }
        /// <summary>Normalizirani naziv za pretragu (bez dijakritika, lowercase) npr. vijecnica, aria mall.</summary>
        public string? DisplayNameSearch { get; set; }
    }
}