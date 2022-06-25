
using System;

public enum EventLogTypes{
    ReservationCreated,

    CarArrivedPTS,
    ButtonPressed,
    Reserved,
    QueueAdded,
    
}

public class EventLog
{
    public int id{ get; set; }
    public string message{ get; set; }
    public EventLogTypes eventLogType { get; set; }
    public  DateTime dateCreated{ get; set; }
}   
