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
        /// <summary>Location display name for search and display (e.g. Aria mall, Vijećnica, Baščaršija).</summary>
        public string? DisplayName { get; set; }
        /// <summary>Normalized name for search (no diacritics, lowercase) e.g. vijecnica, aria mall.</summary>
        public string? DisplayNameSearch { get; set; }
    }
}