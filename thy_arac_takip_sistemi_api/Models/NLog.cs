using System;
namespace thy_arac_takip_sistemi_api.Models
{
    public class NLogItem
    {
        public int id { get; set; }
        public DateTime Date { get; set; }
        public int Level { get; set; }
        public string Message { get; set; }
    }
}
