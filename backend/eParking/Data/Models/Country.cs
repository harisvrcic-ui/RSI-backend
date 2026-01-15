using eParking.Helper.BaseClasses;
using System;

namespace eParking.Data.Models
{
    public class Country : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public byte[]? Flag { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
