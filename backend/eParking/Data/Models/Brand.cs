using eParking.Helper.BaseClasses;
using System;

namespace eParking.Data.Models
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public byte[]? Logo { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
