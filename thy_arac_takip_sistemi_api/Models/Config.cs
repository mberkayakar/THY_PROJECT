using System;
using System.ComponentModel.DataAnnotations;

namespace thy_arac_takip_sistemi_api.Models
{
    //Config values
    public class Config
    {
        [Key]
        public string key { get; set; }
        public string value { get; set; }
    }
}
