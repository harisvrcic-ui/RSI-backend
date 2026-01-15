using eParking.Helper.BaseClasses;
using System;

namespace eParking.Data.Models
{
    public class Gender : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

    }
} 