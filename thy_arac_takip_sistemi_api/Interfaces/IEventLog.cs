using System.Collections.Generic;

interface IEventLog
{
    void CreateEntry(string text,EventLogTypes eventLogType);
    
}