using System;
using System.Collections.Generic;
using System.Numerics;

namespace thy_arac_takip_sistemi_api.Models
{
    public class Door
    {
        public int id { get; set; }
        public int doorNumber { get; set; }
        public string doorName { get; set; }
        //Parti malı-Değerli Kargo-Canlı Hayvan vs.
        public string operationArea { get; set; }
        //isEmpty
        //1 0 N R
        public char state { get; set; }
        public char order { get; set; }

        // if 1 is loaded and full
        public bool isNotEmpty { get; set; }
        public bool isBusy { get; set; }
        //public bool isWaitingForLoading { get; set; }

        //Date of created first
        public DateTime dateCreated { get; set; }
        //Date of last door updated 
        public DateTime? dateUpdated { get; set; }



        //last owner of the door
        public int lastOwnerReservationId { get; set; }
        public string lastOwnerPlate { get; set; }
        //last time when someone come
        public DateTime? dateLastOwned { get; set; }
        //last time someone leave 
        public DateTime? dateLastExit { get; set; }

        public int reservationId { get; set; }

        //Relation with readerModule
        public int readerModuleId { get; set; }
        public ReaderModule readerModule { get; set; }
        //Relation with SCCs
        public List<SCC> sccList { get; set; }


    }
}
