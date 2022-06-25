using System;
namespace thy_arac_takip_sistemi_api.Models
{
    //Sisteme giriş yapan kişilerin loglarının tutulacağı sınıf
    public class LoginLog
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string authority { get; set; }
        public string email { get; set; }
        public DateTime? dateLogin { get; set; }
    }
}
