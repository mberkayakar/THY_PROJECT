using System.Numerics;
using System;
using System.Collections.Generic;

namespace thy_arac_takip_sistemi_api.Models
{
    public class Reservation
    {
        public int id { get; set; }
        //Reservation Type
        //TODO change this to enum
        // 0 = online 1 = manuel creation
        public int? reservationType { get; set; }
        //Agent features
        public long? agentId { get; set; }
        public string agentName { get; set; }
        //Driver features
        public string driverName { get; set; }
        public string driverSurname { get; set; }
        public string driverPhoneNumber { get; set; }
        //Car features
        public string carPlate { get; set; }
        public string carType { get; set; }
        //Date Estimated Arrive of Car
        public DateTime? dateEstimatedArriveStart { get; set; }
        public DateTime? dateEstimatedArriveFinish { get; set; }
        //Date Arrive
        public DateTime? dateCarArrived { get; set; }
        //Logging Date
        public DateTime? dateCreated { get; set; }
        public DateTime? dateUpdated { get; set; }
        //States of reservation
        public bool? isArrived { get; set; }
        public bool? isUnload { get; set; }
        public bool? isWaiting { get; set; }

        public bool? isActive { get; set; }

        public string sccText { get; set; }

        public int? doorNumber { get; set; }

        public List<AWB> awbList { get; set; }


        public DateTime? dateDoorAssigned { get; set; }

    }
    //AWB Code
    public class AWB
    {
        public int id { get; set; }
        public string awbText { get; set; }
        //Properties
        public int? weight { get; set; }
        public string weightUnit { get; set; }
        public int? pieces { get; set; }
        public string destination { get; set; }
        public string origin { get; set; }
        public string handlingCode { get; set; }
        public string flightDetails { get; set; }
        public DateTime? dateFlight { get; set; }


        public string sccText { get; set; }
        public List<SCCText> sccList { get; set; }

        public int reservationId { get; set; }
        public Reservation reservation { get; set; }
    }
    public class SCCText
    {
        public int id { get; set; }
        public string sccText { get; set; }

        public int awbId { get; set; }
    }
}
