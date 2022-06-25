using System.Linq;

public class DashboardInformation
{
    public int reservationCount { get; set; }
    public int finishedCount { get; set; }
    public int waitingCount { get; set; }
    public int importCount { get; set; }
    public int exportCount { get; set; }
    public int ichatCount { get; set; }
    public int doorAvailableCount { get; set; }
    public int doorBusyCount { get; set; }
    public int doorReservedCount { get; set; }

    public int awbCount { get; set; }
    public int pieceCount { get; set; }
    public int totalWeight { get; set; }


    //
    public IQueryable<EventLog> events { get; set; }

}