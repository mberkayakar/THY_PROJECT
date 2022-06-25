using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class DoorRepo : IDoor
    {
        private readonly MyDbContext db;
        private readonly ISCC _scc;


        public DoorRepo(MyDbContext _db, ISCC sc)
        {
            db = _db;
            _scc = sc;

        }
        IQueryable<Door> IDoor.GetDoorAll => db.Doors.Include(e => e.sccList).OrderBy(e => e.doorNumber);


        //TODO log
        public POJO MatchDoorAndSCC(DoorAndSCC doorAndScc)
        {

            POJO model = new POJO
            {
                Flag = false
            };

            try
            {
                SCC scc = _scc.GetSCCWithText(doorAndScc.sccText);
                Door door = db.Doors.Where((e) => e.doorNumber == doorAndScc.doorNumber).FirstOrDefault();
                //
                if (door.sccList == null)
                {
                    door.sccList = new List<SCC>();
                }
                door.sccList.Add(scc);
                if (scc.doorList == null)
                {
                    scc.doorList = new List<Door>();
                }
                scc.doorList.Add(door);


                //If anyone has in query make reservation for her
                try
                {
                    WaitingQueue waiting = db.WaitingQueues.Include(e=>e.reservation).FirstOrDefault(e => e.reservation.sccText == scc.code);

                    if (waiting != null)
                                {

                                    door.order = 'R';
                                    door.reservationId = waiting.reservation.id;
                                    door.dateLastOwned = DateTime.Now;
                                    door.lastOwnerPlate = waiting.reservation.carPlate;
                                    door.dateUpdated = DateTime.Now;
                                    door.isBusy = true;
                                    ///Beklemedeki rezervasyonun durumlarını değiştir ve
                                    waiting.reservation.isWaiting = false;
                                    waiting.reservation.doorNumber = door.doorNumber;
                                    waiting.reservation.dateDoorAssigned = DateTime.Now;
                                    waiting.reservation.dateUpdated = DateTime.Now;


                                    DoorAssign assign = new DoorAssign
                                    {
                                        dateAssign = DateTime.Now,
                                        doorNo = door.doorNumber,
                                        plate = waiting.reservation.carPlate,
                                        reservationId = waiting.reservation.id,
                                    };
                 
                                    db.DoorAssigns.Add(assign);

                                    db.WaitingQueues.Remove(waiting);

                                }
                }
                catch (System.Exception e)
                {
                    model.Message = e.ToString();

            return model;

                }

                db.SaveChanges();
                model.Flag = true;
                model.Message = "Kapılar eşleştirildi";

            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }

            return model;
        }

        Door IDoor.GetDoorAvaliableWithSCC(SCC scc)
        {

            Door door =db.Doors.Where(e => e.sccList.Contains(scc) && e.state == '0' && e.order == '0').FirstOrDefault();


            if (door == null)
            {
                return null;
            }
            return door;

        }

        public POJO ModifyDoor(Door door)
        {
            POJO model = new POJO { Flag = false };
            Door _door = db.Doors.Where((e) => e.id == door.id).FirstOrDefault();

            try
            {
                if (_door != null)
                {
                    db.Entry(door).CurrentValues.SetValues(_door);
                    db.SaveChanges();
                    model.Flag = true;
                    model.Message = "Başarıyla Güncellendi";
                    model.Id = _door.id;
                }
                else
                {
                    model.Message = "Böyle bir kapı bulunamadı";

                }

            }
            catch (Exception e)
            {
                model.Message = e.ToString();


            }
            return model;
        }

        POJO IDoor.MatchDoorAndSCCList(DoorAndSCCList doorAndScc)
        {
            POJO model = new POJO { Flag = false };

            try
            {

                Door door = db.Doors.Include(e => e.sccList).FirstOrDefault(e => e.doorNumber == doorAndScc.doorNumber);     //Clear
                door.sccList.Clear();
                db.SaveChanges();

                foreach (var item in doorAndScc.sccTextList)
                {
                    try
                    {



                        MatchDoorAndSCC(new DoorAndSCC { doorNumber = doorAndScc.doorNumber, sccText = item });

                    }
                    catch (System.Exception e)
                    {
                           model.Message = e.ToString();
                        return model;
                    }
                }
                model.Flag = true;
                model.Message = "Başarılı";
            }
            catch (System.Exception e)
            {
                model.Message = e.ToString();
            }
            return model;
        }

        POJO IDoor.RemoveReservation(int doorId)
        {
            POJO model = new POJO { Flag = false };
            try
            {
                Door door = db.Doors.FirstOrDefault(e => e.id == doorId);
                if (door == null)
                {
                    model.Message = "Bu id ile bir kapı bulunamadı id: " + doorId.ToString();
                    model.Id = doorId;
                    return model;
                }
                door.reservationId = 0;
                door.isBusy = false;
                door.isNotEmpty = false;
                door.order = '0';
                door.state = '0';
                door.dateUpdated = DateTime.Now;
                db.SaveChanges();
                model.Message = "Başarıyla rezervasyon kaldırıldı";
                model.Flag = true;


            }
            catch (System.Exception e)
            {
                model.Message = e.ToString();
            }
            return model;
        }

        POJO IDoor.MakeReservationManual(int doorId, int reservationId)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                Door door = db.Doors.FirstOrDefault(e => e.id == doorId);
                Reservation res = db.Reservations.FirstOrDefault(e => e.id == reservationId);
                if (door == null || res == null)
                {
                    model.Message = "Kapı ya da rezervasyon null";
                    return model;
                }
                door.reservationId = reservationId;
                door.dateLastOwned = DateTime.Now;
                door.lastOwnerPlate = res.carPlate;
                door.order = 'R';
                door.dateUpdated = DateTime.Now;
                door.isBusy = true;
                door.isNotEmpty = false;

                res.doorNumber = door.doorNumber;
                res.dateUpdated = DateTime.Now;
                res.isArrived = true;
                res.dateDoorAssigned = DateTime.Now;

                db.SaveChanges();
                model.Message = "Başarıyla rezervasyon yapıldı";
                model.Flag = true;

            }
            catch (System.Exception e)
            {
                model.Message = e.ToString();
            }
            return model;
        }
    }
}