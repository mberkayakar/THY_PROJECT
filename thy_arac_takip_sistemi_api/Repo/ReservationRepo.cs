using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class ReservationRepo : IReservation
    {
        private readonly MyDbContext db;
        private readonly ISCC scc;
        private readonly IDoor door;


        public ReservationRepo(MyDbContext _db, ISCC sc, IDoor dr)
        {
            db = _db;
            door = dr;
            scc = sc;
        }


        IQueryable<Reservation> IReservation.GetReservationsNotDoorAssigned => db.Reservations.Include(s => s.awbList).ThenInclude(d => d.sccList).Where(e => e.doorNumber == null);

        //Get all reservations
        List<Reservation> IReservation.GetReservations()
        {
            var list = db.Reservations.Include(s => s.awbList).ThenInclude(d => d.sccList).Where(e => e.reservationType != 1).ToList();
            return list;
        }
        //Get all reservations of agent 
        List<Reservation> IReservation.GetActiveReservationsWithAgentId(long agentId)
        {

            //isUnload = false
            //rezervasyon günü ve sonrakileri gösterir
            IQueryable<Reservation> reservations = db.Reservations.Where((e) => e.agentId == agentId && e.isUnload == false)
            .Include(s => s.awbList).ThenInclude(d => d.sccList);
            List<Reservation> list = new List<Reservation>();
            foreach (var e in reservations)
            {
                if (e.dateEstimatedArriveFinish != null)
                    if (DateTime.Today <= (new DateTime(e.dateEstimatedArriveFinish.Value.Year, e.dateEstimatedArriveFinish.Value.Month, e.dateEstimatedArriveFinish.Value.Day)))
                    {

                        list.Add(e);
                    }
            }
            return list;
        }
        List<Reservation> IReservation.GetAllReservationsFinishedWithAgentId(long agentId)
        {

            //isUnload = true
            //bugunden öncekileri gösterir
            IQueryable<Reservation> reservations = db.Reservations.Where((e) => e.agentId == agentId && e.isUnload == true)
            .Include(s => s.awbList).ThenInclude(d => d.sccList);
            List<Reservation> list = new List<Reservation>();
            /*  foreach (var e in reservations)
             {   
                 if(e.dateEstimatedArriveFinish!=null)
                 if(DateTime.Today >= (new DateTime(e.dateEstimatedArriveFinish.Value.Year,e.dateEstimatedArriveFinish.Value.Month,e.dateEstimatedArriveFinish.Value.Day))){

                     list.Add(e);
                 }
             } */
            return list;
        }
        List<Reservation> IReservation.GetPassiveReservationsWithAgentId(long agentId)
        {

            //isActive = false
            //rezervasyon günü ve sonrakileri gösterir
            IQueryable<Reservation> reservations = db.Reservations.Where((e) => e.agentId == agentId && e.isUnload == false)
            .Include(s => s.awbList).ThenInclude(d => d.sccList);
            List<Reservation> list = new List<Reservation>();
            foreach (var e in reservations)
            {
                if (e.dateEstimatedArriveFinish != null)
                    if (DateTime.Today > (new DateTime(e.dateEstimatedArriveFinish.Value.Year, e.dateEstimatedArriveFinish.Value.Month, e.dateEstimatedArriveFinish.Value.Day)))
                    {

                        list.Add(e);
                    }
            }
            return list;
        }
        /* 
        && (
                
               e.dateEstimatedArriveStart.Value.Day == DateTime.Now.Day &&  e.dateEstimatedArriveStart.Value.Month ==  DateTime.Now.Month &&  e.dateEstimatedArriveStart.Value.Year ==  DateTime.Now.Year
            ) */
        bool isSameDay(DateTime dt, DateTime dt2)
        {
            return
            dt.Day == dt2.Day && dt.Month == dt2.Month && dt.Year == dt2.Year;
        }
        //Get reservation from plate- return first and if its arrived ?!
        Reservation IReservation.GetReservationFromPlate(string plate)
        {
            //TODO is arrived? 
            Reservation model = db.Reservations.Where((e) => e.carPlate == plate).Include(s => s.awbList).ThenInclude(d => d.sccList).FirstOrDefault();
            return model;
        }
        //Get reservation for known id
        Reservation IReservation.GetReservationFromId(int id)
        {
            //TODO is arrived? 
            Reservation model = db.Reservations.Where((e) => e.id == id).FirstOrDefault();
            return model;
        }
        //Get reservations between two arrive date,
        IQueryable<Reservation> IReservation.GetReservationsBetweenTwoArriveDate(DateTime start, DateTime finish)
        {
            //Datecararrived should be earlier then finish and after then start. And of course it should be arrived = True
            IQueryable<Reservation> reservations = db.Reservations.Where((e) => e.dateCarArrived <= finish && e.dateCarArrived >= start && e.isArrived == true).Include(s => s.awbList).ThenInclude(d => d.sccList);
            return reservations;
        }
        //Create reservation
        POJO IReservation.CreateReservation(Reservation reservation)
        {
            POJO model = new POJO { Flag = false };
            reservation.isActive = true;
            reservation.dateCreated = DateTime.Now;
            reservation.dateUpdated = DateTime.Now;

            try
            {
                db.Reservations.Add(reservation);
                //!
                // button entry var ise kapı atanacak kapı da yoksa waiting kısmına aktarılacak
                ButtonEntry entry = db.ButtonEntries.FirstOrDefault(e => e.plate == reservation.carPlate);
                //Entry var ise ve 2 saat içerisinde girmiş ise
                //Config tablosundan gelen button delay ile kontrol yapılıyor
                //Convert.ToInt32(db.Configs.FirstOrDefault(e => e.key == "RESERVATION_DELAY"))
                //?Button girişiyle girip ardından rezervasyon yapan kişilerin oncelikli olması icin 
                //!Export olanlar icin sadece artıK! 
                if (entry != null && entry.buttonNo == 0 && DateTime.Now.Subtract(entry.dateLogin.Value).TotalSeconds < Convert.ToInt32(db.Configs.FirstOrDefault(e => e.key == "BUTTON_DELAY").value))
                {
                    //TODO boş kapı varsa ata


                    reservation.isArrived = true;
                    reservation.dateCarArrived = DateTime.Now;



                    //TODO scc listesi karsılastırılacak awbler arasında
                    List<SCC> maxSCCList = new List<SCC>();

                    foreach (AWB awb in reservation.awbList)
                    {

                        SCC maxScc = new SCC
                        {
                            strength = 999999
                        };

                        foreach (SCCText a in awb.sccList)
                        {
                            SCC sccItem = scc.GetSCCWithText(scc: a.sccText);

                            //TODO sccleri textten 
                            if (sccItem.strength < maxScc.strength)
                            {
                                maxScc = sccItem;
                            }
                        }
                        maxSCCList.Add(maxScc);
                    }
                    bool isMix = false;

                    if (maxSCCList.Count > 1)
                    {
                        for (int i = 0; i < maxSCCList.Count; i++)
                        {
                            for (int j = i; j < maxSCCList.Count; j++)
                            {

                                if (maxSCCList[i].code != maxSCCList[j].code)
                                {
                                    isMix = true;
                                }
                            }
                        }
                    }
                    string sccText = maxSCCList.FirstOrDefault().code;
                    if (isMix)
                    {
                        sccText = "JOKER";
                    }



                    //TODO  
                    //SCC kodunda sccyi get 


                    SCC sccObj = scc.GetSCCWithText(scc: sccText);
                    if (sccObj == null)
                    {
                        model.Message = "Rezervasyon var fakat geçerli SCC yok";
                        return model;
                    }

                    //TODO  
                    //SCC kodu ile uygun kapı var mı kontrol edilecek
                    //Kapı get et

                    try
                    {

                        Door myDoor = door.GetDoorAvaliableWithSCC(sccObj);
                        if (myDoor != null)
                        {
                            if (reservation.doorNumber != null)
                            {
                                model.Message = "Atanmış bir kapı bulunmaktadır";
                                return model;
                            }
                            model.Flag = true;
                            model.Message = "Rezervasyon var. Scc text:" + sccText + " verilebilecek ilk kapı : " + (myDoor.doorNumber + 1);

                            myDoor.order = 'R';
                            reservation.doorNumber = myDoor.doorNumber;
                            myDoor.reservationId = reservation.id;


                            reservation.dateDoorAssigned = DateTime.Now;
                            reservation.dateUpdated = DateTime.Now;

                            db.SaveChanges();
                        }
                        else
                        {
                            if (reservation.isWaiting == false)
                            {
                                //TODO kuyruğa eklencek
                                WaitingQueue waiting = new WaitingQueue
                                {
                                    dateCreated = DateTime.Now,
                                    reservation = reservation,
                                    reservationId = reservation.id,

                                };
                                db.WaitingQueues.Add(waiting);
                                reservation.isWaiting = true;
                                db.SaveChanges();

                                model.Message = "Bekleme salonunda alınan rezervasyon kuyruğa eklendi.";
                                return model;
                            }
                            else
                            {
                                model.Message = "Rezervasyon beklemede";
                                return model;
                            }

                        }


                    }

                    catch (Exception ex)
                    {
                        model.Message = ex.ToString();

                    }
                }
                else
                {
                    db.SaveChanges();
                    model.Message = "Başarıyla Eklendi";
                }
                model.Flag = true;
                model.Id = reservation.id;




            }
            catch (Exception e)
            {
                model.Flag = false;
                model.Message = e.ToString();

            }
            return model;
        }
        //Modify Reservation
        POJO IReservation.ModifyReservation(Reservation reservation)
        {
            POJO model = new POJO { Flag = false };
            Reservation res = db.Reservations.Where((e) => e.id == reservation.id).FirstOrDefault();
            reservation.dateUpdated = DateTime.Now;
            try
            {
                if (res != null)
                {
                    db.Entry(res).CurrentValues.SetValues(reservation);
                    db.SaveChanges();
                    model.Flag = true;
                    model.Message = "Başarıyla Güncellendi";
                    model.Id = reservation.id;
                }
                else
                {
                    model.Message = "Böyle bir kayıt bulunamadı";

                }

            }
            catch (Exception e)
            {
                model.Message = e.ToString();


            }
            return model;
        }
        //Delete reservation
        POJO IReservation.DeleteReservation(int id)
        {
            POJO model = new POJO { Flag = false };

            try
            {
                Reservation res = db.Reservations.Where((e) => e.id == id).FirstOrDefault();
                if (res == null)
                {
                    model.Message = "Böyle bir kayıt bulunamadı";
                    model.Id = id;
                }
                db.Reservations.Remove(res);
                db.SaveChanges();
                model.Flag = true;
                model.Message = "Başarıyla silindi";
                model.Id = id;
            }
            catch (Exception e)
            {
                model.Message = e.ToString();
                model.Id = id;
            }
            return model;

        }


        public POJO DeleteReservations(List<int> idList)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                foreach (var item in idList)
                {
                    Reservation pts = db.Reservations.FirstOrDefault(e => e.id == item);
                    if (pts != null)
                    {
                        db.Reservations.Remove(pts);
                        db.SaveChanges();

                        model.Flag = true;
                    }
                }

            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }
            return model;
        }

    }
}
