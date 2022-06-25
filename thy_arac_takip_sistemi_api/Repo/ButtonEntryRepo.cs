using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;
using thy_arac_takip_sistemi_api.Utils;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class ButtonEntryRepo : IButtonEntry
    {
        private readonly MyDbContext db;
        private readonly IReservation reservation;
        private readonly ISCC scc;
        private readonly IPTS pts;

        public ButtonEntryRepo(MyDbContext _db, IReservation _ress, ISCC _scc, IPTS _pts)
        {
            db = _db;
            scc = _scc;
            pts = _pts;
            reservation = _ress;
        }

        public IQueryable<ButtonEntry> GetButtonEntries => db.ButtonEntries;

        public POJO AddButtonEntry(ButtonEntry button, IFormFile file)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            button.plate = button.plate.ToUpper();
            //Eğer aktif rezervasyon var ise bu plakaya atanmış alt satıra gecme -- 
            Reservation _res = db.Reservations.FirstOrDefault(e => e.isUnload == false && e.carPlate.ToUpper() == button.plate && e.isActive == true);
            if (_res != null)
            //? Reservasyon var ise
            {
                POJO _model = pts.CheckReservationPTS(
                             new PTS
                             {
                                 doorNo = button.doorNo,
                                 plate = button.plate,
                                 dateLogin = button.dateLogin,
                             }, file);
                return _model;
            }
            //Else ise button entry yap
            else
            {
                try
                {
                    // Gelen kişileri log için dbye kaydediyoruz
                    PTS ptsLog = new PTS
                    {
                        plate = button.plate,
                        dateLogin = DateTime.Now,
                        doorNo = button.doorNo,
                        isButtonEntry = true,
                        buttonNo = button.buttonNo,
                        fileName = file.FileName,
                    };
                    //Log 
                    db.PTSLogs.Add(ptsLog);
                    //
                    //Save imageƒ
                    ImageSaver saver = new ImageSaver();
                    saver.SaveImage(file: file);


                    button.dateLogin = DateTime.Now;
                    db.ButtonEntries.Add(button);
                    switch (button.buttonNo)
                    {
                        //EXPORT ise
                        case 0:
                            db.SaveChanges();
                            model.Flag = true;
                            model.Message = "*BEKLEME-ALANI#";
                            break;
                        case 1:
                            //create reservation for import and create
                            //if there is no busy door assign 
                            string type = "IMPORT";
                            List<AWB> awb = new List<AWB>{
                             new AWB()
                        {
                            awbText = type,
                            sccText = type,
                            sccList = new List<SCCText>(){
                                 new SCCText(){sccText= type}
                            }
                        }
                        };
                            POJO res = reservation.CreateReservation(
                                new Reservation()
                                {
                                    reservationType = 1,//!manual
                                    awbList = awb,
                                    carPlate = button.plate,
                                    dateCreated = DateTime.Now,
                                    isArrived = true,
                                    isActive = true,
                                    isUnload = false,
                                    isWaiting = false,
                                    sccText = type,
                                }
                            );
                            POJO ptsRes = pts.CheckReservationPTSWithoutPTSLOG(
                                new PTS()
                                {
                                    doorNo = button.doorNo,
                                    plate = button.plate,
                                    dateLogin = button.dateLogin,
                                    fileName = file.FileName,
                                    buttonNo = 1,
                                },
                                file
                            );
                            System.Console.WriteLine(ptsRes);
                            model.Flag = true;


                            break;
                        case 2:
                            //create reservation for ichat and create
                            //if there is no busy door assign 

                            string type_ichat = "ICHAT";
                            List<AWB> awb_ichat = new List<AWB>{
                             new AWB()
                        {

                            awbText = type_ichat,
                            sccText = type_ichat,
                            sccList = new List<SCCText>(){
                                 new SCCText(){sccText= type_ichat}
                            }
                        }
                        };
                            POJO res_ichat = reservation.CreateReservation(
                                new Reservation()
                                {
                                    reservationType = 1,//!manual
                                    awbList = awb_ichat,
                                    carPlate = button.plate,
                                    dateCreated = DateTime.Now,
                                    isArrived = true,
                                    isActive = true,
                                    isUnload = false,
                                    isWaiting = false,
                                    sccText = type_ichat,
                                }
                            );
                            POJO ptsRes_ichat = pts.CheckReservationPTSWithoutPTSLOG(
                                new PTS()
                                {
                                    doorNo = button.doorNo,
                                    plate = button.plate,
                                    dateLogin = button.dateLogin,
                                    fileName = file.FileName,
                                    buttonNo = 1,
                                },
                                file
                            );
                            System.Console.WriteLine(ptsRes_ichat);
                            model.Flag = true;

                            break;


                    }

                }






                catch (Exception ex)
                {
                    model.Message = ex.ToString();
                }

            }
            return model;
        }


        public POJO DeleteButtonEntries(List<int> idList)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                foreach (var item in idList)
                {
                    ButtonEntry entry = db.ButtonEntries.FirstOrDefault(e => e.id == item);
                    if (entry != null)
                    {
                        db.ButtonEntries.Remove(entry);
                        db.SaveChanges();

                        model.Message = "Başarıyla silindi.";
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

        public POJO DeleteButtonEntry(int id)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                ButtonEntry entry = db.ButtonEntries.FirstOrDefault(e => e.id == id);
                if (entry != null)
                {
                    db.ButtonEntries.Remove(entry);
                    db.SaveChanges();

                    model.Message = "Başarıyla silindi.";
                    model.Flag = true;
                }
                else
                {
                    model.Message = "Böyle bir kayıt bulunamadı";

                }
            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }
            return model;
        }

        public ButtonEntry UpdateButtonEntry(ButtonEntry btn)
        {
            try
            {
                ButtonEntry _buttonEntry = db.ButtonEntries.FirstOrDefault(e => e.id == btn.id);
                if (_buttonEntry != null)
                {
                    _buttonEntry.plate = btn.plate;
                }
                db.SaveChanges();
                return _buttonEntry;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
