using eParking.Helper.BaseClasses;
using System;

namespace eParking.Data.Models
{
    public class ReservationType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }

    }
}