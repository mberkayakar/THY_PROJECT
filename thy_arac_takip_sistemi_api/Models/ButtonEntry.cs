using System;
namespace thy_arac_takip_sistemi_api.Models
{
    //Buton ile giriş yapan kişilerin listesini tuttuğumuz kısım
    public class ButtonEntry
    {
        public int id { get; set; }
        public string plate { get; set; }
        public int buttonNo { get; set; }
        public int doorNo { get; set; }
        public DateTime? dateLogin { get; set; }
    }
}
