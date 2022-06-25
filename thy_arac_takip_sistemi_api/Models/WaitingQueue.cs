using System;
using System.Numerics;

namespace thy_arac_takip_sistemi_api.Models
{
    public class WaitingQueue
    {
        public int id { get; set; }
        public DateTime dateCreated{get;set;}

        public Reservation reservation { get; set; }
        public int reservationId{ get; set; }
    }
}
