using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class ReaderRepo : IReaderModule
    {
        private readonly MyDbContext db;
        private readonly ISCC sccContext;
        public ReaderRepo(MyDbContext _db, ISCC sc)
        {
            db = _db;
            sccContext = sc;
        }

        IQueryable<ReaderModule> IReaderModule.GetReaderModulesAll => db.ReaderModules.Include((e) => e.doorList);

        ReaderModule IReaderModule.GetReaderModule(int id)
        {
            ReaderModule model = db.ReaderModules.Where((e) => e.id == id).Include(e => e.doorList).FirstOrDefault();
            return model;
        }
        //Create reader module 
        POJO IReaderModule.CreateReaderModule(ReaderModule readerModule)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                //we will be added doors to this 
                List<Door> doors = new List<Door>();
                //stating number 30-50
                //30
                int start = readerModule.doorCountStart;
                //50
                int finish = readerModule.doorCountFinish;
                //between these two numbers we will be add list- and give each one id
                // if we start 30 , first door we have is doorId=30
                for (int i = start; i <= finish; i++)
                {
                    doors.Add
                        (
                        new Door
                        {
                            //Initial values
                            order = '0',
                            state = '0',
                            isBusy = false,
                            isNotEmpty = false,
                            dateCreated = DateTime.Now,
                            doorNumber = i,
                        }
                        );
                }
                //after added the list all doors we are creating object and added it too
                ReaderModule reader = new ReaderModule
                {
                    dateCreated = readerModule.dateCreated != null ? readerModule.dateCreated : DateTime.Now,
                    readerIp = readerModule.readerIp,
                    readerName = readerModule.readerName,
                    doorCountFinish = readerModule.doorCountFinish,
                    doorCountStart = readerModule.doorCountStart,
                    readerId = readerModule.readerId,
                    //Doors
                    doorList = doors,
                };
                db.ReaderModules.Add(reader);
                db.SaveChanges();
                model.Flag = true;
                model.Message = "Başarıyla Reader Module oluşturuldu";
                model.NumberOfRows = doors.Count();

            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }

            return model;

        }
        POJO IReaderModule.GetOrders(int id)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                //Get reader with given id
                ReaderModule readerModule = db.ReaderModules.Where((e) => e.readerId == id).FirstOrDefault();
                //First door is the starting module in there and we will set this
                //30-50
                int start = readerModule.doorCountStart;
                //50
                int finish = readerModule.doorCountFinish;
                //between these two numbers we will be add list- and give each one id
                // if we start 30 , first door we have is doorId=30
                string orders = "";
                for (int i = start; i <= finish; i++)
                {
                    Door door = db.Doors.Where(e => e.doorNumber == i).FirstOrDefault();
                    orders += door.order;
                }
                System.Console.WriteLine(orders);
                //Order
                model.Message = orders;
                model.Flag = true;

                model.Id = id;
                model.NumberOfRows = readerModule.doorCountFinish - readerModule.doorCountStart;
            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }
            return model;
        }
        //For update door states. State variable of door setted what it gets from controller card
        POJO IReaderModule.UpdateDoorStates(int id, string doorData)
        {

            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                //Get reader with given id
                ReaderModule readerModule = db.ReaderModules.Where((e) => e.readerId == id).FirstOrDefault();
                //First door is the starting module in there and we will set this
                //30-50
                int i = readerModule.doorCountStart;
                string order = "";
                //

                foreach (char item in doorData)
                {
                    //Get door
                    Door door = db.Doors.Where(e => e.doorNumber == i).Include(e => e.sccList).FirstOrDefault();
                    Reservation res = db.Reservations.FirstOrDefault(e => e.id == door.reservationId);

                    //Change the state
                    switch (item)
                    {

                        case '1':
                            //If door state changed set order =

                            if (door.order == 'R' && door.state == 'R')
                            {
                                door.order = '0';
                                res.isUnload = true;
                                res.dateUpdated = DateTime.Now;
                            }
                            door.state = '1';

                            break;
                        case '0':
                            if (door.order == 'R' && door.state == 'R')
                            {
                                door.order = '0';
                            }

                            //kapı bosalınca kuyrukta res varsa onu ata
                            else if (door.order == '0')
                            {

                                if (res != null)

                                    res.isUnload = true;




                                //TODO kuyruktan en uygunu bul
                                List<string> list = door.sccList.Select(s => s.code).ToList();
                                WaitingQueue waiting = db.WaitingQueues.Include(e => e.reservation).FirstOrDefault(f => list.Contains(f.reservation.sccText));
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


                                    //? panel     
                                    db.DoorAssigns.Add(assign);

                                    db.WaitingQueues.Remove(waiting);

                                    db.SaveChanges();
                                }
                                //TODO rezerve et kapıyı
                                door.state = '0';

                            }
                            break;
                        case 'R':
                            door.state = 'R';
                            if (door.state == 'R' && door.order == 'R')
                            {
                                int delay = Convert.ToInt32(db.Configs.FirstOrDefault(e => e.key == "RESERVATION_DELAY").value);
                                //Eğer delay süresi kadar bir vakit geçmişse, kapı durumlarını 0a çekip reserve durumlarını güncelliyoruz
                                if (DateTime.Now.Subtract(res.dateDoorAssigned.Value).TotalSeconds > delay)
                                {
                                    door.order = '0';
                                    door.state = '0';
                                    res.doorNumber = null;
                                    res.isActive = false;
                                    //TODO Queue kısmına eklenecek mi
                                    //door assign kaldır
                                }
                            }
                            //  door.order = '0';
                            break;
                        case 'N':
                            door.state = 'N';
                            break;
                        default:
                            break;
                    }
                    order += door.order;


                    db.SaveChanges();
                    //++
                    i++;
                }

                //Order
                model.Message = order;
                model.Flag = true;

                model.Id = id;
                model.NumberOfRows = readerModule.doorCountFinish - readerModule.doorCountStart;
            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }
            return model;
        }
        //Delete reader
        POJO IReaderModule.DeleteReaderModule(int id)
        {
            POJO model = new POJO { Flag = false };

            try
            {
                ReaderModule res = db.ReaderModules.Where((e) => e.id == id).FirstOrDefault();
                if (res == null)
                {
                    model.Message = "Böyle bir kayıt bulunamadı";
                    model.Id = id;
                }
                db.ReaderModules.Remove(res);
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





    }
}