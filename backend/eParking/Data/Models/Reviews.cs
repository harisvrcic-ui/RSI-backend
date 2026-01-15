using eParking.Helper.BaseClasses;
using System;

namespace eParking.Data.Models
{
    public class Reviews : BaseEntity
    {
        public int UserId { get; set; }
        public int ReservationId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;

    }
}