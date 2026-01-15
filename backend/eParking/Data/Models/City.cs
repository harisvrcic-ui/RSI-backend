using eParking.Helper.BaseClasses;
using System;

namespace eParking.Data.Models
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
} 