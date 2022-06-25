using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;
using thy_arac_takip_sistemi_api.Repo;

namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        //   private readonly ILogger<LogsController> _logger;
        //     private readonly ILogs logs;
        private readonly MyDbContext db;
        public DashboardController(MyDbContext _db)
        {
            db = _db;
        }


        [HttpGet]
        public IActionResult Get()
        {
            DashboardInformation info = new DashboardInformation();
            //!Door states
            info.doorAvailableCount = db.Doors.Where(e => e.state == '0' && e.order == '0').Count();
            info.doorBusyCount = db.Doors.Where(e => e.state == '1').Count();
            info.doorReservedCount = db.Doors.Where(e => e.order == 'R').Count();

            //!Reservation states
            info.reservationCount = db.Reservations.Where(e => e.dateCreated.Value.Date == DateTime.Now.Date && e.reservationType != 1).Count();
            info.finishedCount = db.Reservations.Where(e => e.isUnload == true && e.dateDoorAssigned.Value.Date == DateTime.Now.Date).Count();
            info.waitingCount = db.WaitingQueues.Count();
            //Where(e => e.dateCreated.Date == DateTime.Now.Date)
            //!Button states
            info.exportCount = db.ButtonEntries.Where(e => e.buttonNo == 0 && e.dateLogin.Value.Date == DateTime.Now.Date).Count();
            info.importCount = db.ButtonEntries.Where(e => e.buttonNo == 1 && e.dateLogin.Value.Date == DateTime.Now.Date).Count();
            info.ichatCount = db.ButtonEntries.Where(e => e.buttonNo == 2 && e.dateLogin.Value.Date == DateTime.Now.Date).Count();

            //!cargo states
            //info.awbCount;
            List<Reservation> reservations = db.Reservations.Where(e => e.dateEstimatedArriveStart.Value.Date == DateTime.Now.Date).Include(e => e.awbList).ToList();
            int count = 0;
            int weight = 0;
            int piece = 0;
            foreach (var item in reservations)
            {
                foreach (var awb in item.awbList)
                {
                    count++;
                    piece += awb.pieces.Value;
                    weight += awb.weight.Value;
                }
            }
            info.awbCount = count;
            info.totalWeight = weight;
            info.pieceCount = piece;
            info.events = GetEvents();
            return Ok(info);
        }
        [HttpGet]
        [Route("events")]
        public IActionResult GetEventsApi()
        {
            return Ok(db.EventLogs.Where(e => e.dateCreated.Date == DateTime.Now.Date).OrderByDescending(e => e.dateCreated));
        }

        public IQueryable<EventLog> GetEvents()
        {
            return (db.EventLogs.Where(e => e.dateCreated.Date == DateTime.Now.Date).OrderByDescending(e => e.dateCreated));
        }
    }
}
