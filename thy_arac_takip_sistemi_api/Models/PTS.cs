using System;
namespace thy_arac_takip_sistemi_api.Models
{
    //Plaka tanıma sistemininden geçen her arabanın kayıtlarının log olarak tutulduğu kısım.
    public class PTS
    {
        public int id { get; set; }
        public int doorNo { get; set; }
        public bool isButtonEntry { get; set; }
        public int buttonNo { get; set; }
        public string plate { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string type { get; set; }
        public string color { get; set; }
        public DateTime? dateLogin { get; set; }
        public string fileName{get;set;}
}
}
