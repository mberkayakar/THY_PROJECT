using System;
using System.Numerics;

namespace thy_arac_takip_sistemi_api.Models
{
    public class DoorAssign
    {
         public int id { get; set; }
        public int doorNo { get; set; }
        public string plate { get; set; }
        public int reservationId { get; set; }
        public DateTime? dateAssign { get; set; }
    }
}
