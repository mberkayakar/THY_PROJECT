using System;
using System.Collections.Generic;

namespace thy_arac_takip_sistemi_api.Models
{
    public class SCC
    {
        public int id { get; set; }
        public string code { get; set; }
        //Önem 
        public int strength { get; set; }

        //   public int doorId { get; set; } 
        [Newtonsoft.Json.JsonIgnore]
        public List<Door> doorList { get; set; }
    }
}
