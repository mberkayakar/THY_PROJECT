using System;
using System.Collections.Generic;

namespace thy_arac_takip_sistemi_api.Models
{
    public class ReaderModule
    {
        public int id { get; set; }
        public string readerName { get; set; }
        public int readerId { get; set; }
        public string readerIp { get; set; }
        //1- 0- N-  etc- 110011N
        //Parse data
        public string readerData { get; set; }

        // scope of reader 1-15,50-90 etc.

        public int doorCountStart { get; set; }
        public int doorCountFinish { get; set; }

        public DateTime dateCreated { get; set; }
        public DateTime? dateUpdated { get; set; }
        public DateTime? dateLastRead { get; set; }


        //RL Door
        public List<Door> doorList { get; set; }
    }
}
