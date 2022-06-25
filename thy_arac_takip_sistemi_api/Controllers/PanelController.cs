using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thy_arac_takip_sistemi_api.Models;
using thy_arac_takip_sistemi_api.Repo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace thy_arac_takip_sistemi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PanelController : ControllerBase
    {
        private readonly MyDbContext db;

        public PanelController(MyDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        [Route("formatted")]
        public IActionResult Get(int id)
        {
            List<DoorAssign> doorAssigns = db.DoorAssigns.OrderByDescending(e => e.dateAssign).Take(10).ToList();

            string text = "*";
            foreach (var item in doorAssigns)
            {
                if (item.plate.Length == 7)
                {
                    text += item.plate.ToUpper() + "    " + item.doorNo + "-";

                }
                else if (item.plate.Length == 8)
                {
                    text += item.plate.ToUpper() + "   " + item.doorNo + "-";

                }
            }
            text += "#";

            return Ok(text);
        }
        [HttpGet]
        //LOG this id
        public IActionResult GetList(int id)
        {
            double delay = Convert.ToDouble(db.Configs.FirstOrDefault(e => e.key == "RESERVATION_DELAY").value);

            System.Console.WriteLine(DateTime.Now);
            List<DoorAssign> doorAssigns = db.DoorAssigns.OrderByDescending(e => e.dateAssign).ToList();
            List<DoorAssign> temp = new List<DoorAssign>();
            System.Console.WriteLine(doorAssigns.Count());

            foreach (var item in doorAssigns)

            {
                if (item.dateAssign.Value.AddSeconds(delay) > DateTime.Now)
                {
                    Reservation res = db.Reservations.FirstOrDefault(e => e.id == item.reservationId);
                    if (res.isUnload == false && res.isActive == true)
                    {
                        temp.Add(item);
                    }
                }

            }


            return Ok(temp);
        }/* 
        public bool isHigher(DateTime date, double delay)
        {
            if (DateTime.Now.Subtract(date).TotalSeconds < delay)
            {
                return true;
            }
            else
            {
                return false;
            }
        } */
    }
}
