using System;
using System.Collections.Generic;
using System.Linq;
using thy_arac_takip_sistemi_api.Repo;

public class EventLogRepo : IEventLog
{
    private readonly MyDbContext db;
    public EventLogRepo(MyDbContext _db)
    {
        db = _db;
    }

    public void CreateEntry(string text,EventLogTypes _eventLogType)
    {

        EventLog eventLog = new EventLog
        {
            eventLogType = _eventLogType,
            message = text,
            dateCreated = DateTime.Now,
        };
        db.EventLogs.Add(eventLog);
        db.SaveChanges();

    }

  
}
