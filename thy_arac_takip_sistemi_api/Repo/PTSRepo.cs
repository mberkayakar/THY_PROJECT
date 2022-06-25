using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;
using Microsoft.AspNetCore.Http;
using thy_arac_takip_sistemi_api.Utils;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class PTSRepo : IPTS
    {
        private readonly MyDbContext db;
        private readonly IReservation reservation;
        private readonly IDoor door;
        private readonly ISCC scc;
        public PTSRepo(MyDbContext _db, IReservation rs, IDoor dr, ISCC sc)
        {
            db = _db;
            reservation = rs;
            door = dr;
            scc = sc;
        }

        POJO IPTS.CheckReservationPTS(PTS pts, IFormFile file)
        {

            POJO model = new POJO
            {
                Flag = false
            };

            try
            {
                pts.plate = pts.plate.ToUpper();
                // Gelen kişileri log için dbye kaydediyoruz
                string filename = "";
                if (file != null) { filename = file.FileName; }
                PTS ptsLog = new PTS
                {
                    plate = pts.plate,
                    dateLogin = DateTime.Now,
                    doorNo = pts.doorNo,
                    fileName = filename,
                    isButtonEntry = false
                };
                ImageSaver saver = new ImageSaver();
                saver.SaveImage(file: file);
                //Log added
                db.PTSLogs.Add(ptsLog);
                db.SaveChanges();
                Reservation res = reservation.GetReservationFromPlate(pts.plate);
                if (res != null)
                {
                    //kapı atanmış ise zaten onu göster
                    //üst  üste okuma vs durumlarında
                    if (res.doorNumber != null)
                    {
                        model.Id = res.doorNumber;
                        model.Flag = true;
                        model.Message = "*" + res.carPlate.ToUpper() + "- KAPI " + (res.doorNumber + 1) + "#";
                        return model;
                    }

                    res.isArrived = true;
                    res.dateCarArrived = DateTime.Now;

                    //! scc listesi karsılastırılacak awbler arasında
                    List<SCC> maxSCCList = new List<SCC>();

                    foreach (AWB awb in res.awbList)
                    {

                        SCC maxScc = new SCC
                        {
                            strength = 99999
                        };

                        foreach (SCCText a in awb.sccList)
                        {
                            SCC sccItem = scc.GetSCCWithText(scc: a.sccText);

                            //! sccleri text
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

                    SCC sccObj = scc.GetSCCWithText(scc: sccText);
                    if (sccObj == null)
                    {
                        model.Message = "Rezervasyon var fakat geçerli SCC yok";
                        return model;
                    }
                    res.sccText = sccText;
                    //SCC kodu ile uygun kapı var mı kontrol edilecek
                    //Kapı get et

                    try
                    {

                        Door myDoor = door.GetDoorAvaliableWithSCC(sccObj);
                        //Uygun kapı var mı kontrolü
                        if (myDoor != null)
                        {
                            if (res.doorNumber != null)
                            {
                                model.Message = "Atanmış bir kapı bulunmaktadır";
                                return model;
                            }

                            model.Id = myDoor.doorNumber;
                            model.Flag = true;
                            model.Message = "*" + res.carPlate.ToUpper() + "- KAPI" + (myDoor.doorNumber + 1) + "#";

                            //    model.Message = "Rezervasyon var. Scc text:" + sccText + " verilebilecek ilk kapı : " + myDoor.doorNumber;

                            //Door state changes
                            myDoor.reservationId = res.id;
                            myDoor.lastOwnerPlate = res.carPlate;
                            myDoor.lastOwnerReservationId = res.id;
                            myDoor.dateLastOwned = DateTime.Now;
                            myDoor.order = 'R';

                            res.doorNumber = myDoor.doorNumber;
                            res.dateDoorAssigned = DateTime.Now;
                            res.dateUpdated = DateTime.Now;


                            DoorAssign assign = new DoorAssign
                            {
                                dateAssign = DateTime.Now,
                                doorNo = myDoor.doorNumber,
                                plate = res.carPlate,
                                reservationId = res.id,
                            };
                            db.DoorAssigns.Add(assign);


                            db.SaveChanges();
                        }
                        //Uygun kapı yok ise beklemeye gönder
                        else
                        {
                            //onceden beklemeye alınmıs mı diye kontrol et
                            if (res.isWaiting == false)
                            {
                                // Beklemede degil ise ve uygun kapı yok ise beklemeye alınacak
                                WaitingQueue waiting = new WaitingQueue
                                {
                                    dateCreated = DateTime.Now,
                                    reservation = res,
                                    reservationId = res.id,

                                };
                                db.WaitingQueues.Add(waiting);
                                res.isWaiting = true;
                                db.SaveChanges();

                                model.Message = "*BEKLEME-ALANI#";
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
                    model.Message = "* BUTONA- BASINIZ#";
                }
            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }

            return model;
        }
        POJO IPTS.CheckReservationPTSWithoutPTSLOG(PTS pts, IFormFile file)
        {

            POJO model = new POJO
            {
                Flag = false
            };

            try
            {

                Reservation res = reservation.GetReservationFromPlate(pts.plate);
                if (res != null)
                {
                    if (res.doorNumber != null)

                    {
                        model.Id = res.doorNumber;
                        model.Flag = true;
                        model.Message = "*" + res.carPlate.ToUpper() + "- KAPI " + (res.doorNumber + 1) + "#";
                        return model;
                    }
                    //Save image

                    res.isArrived = true;
                    res.dateCarArrived = DateTime.Now;

                    //! scc listesi karsılastırılacak awbler arasında
                    List<SCC> maxSCCList = new List<SCC>();

                    foreach (AWB awb in res.awbList)
                    {

                        SCC maxScc = new SCC
                        {
                            strength = 99999
                        };

                        foreach (SCCText a in awb.sccList)
                        {
                            SCC sccItem = scc.GetSCCWithText(scc: a.sccText);

                            //! sccleri text
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

                    SCC sccObj = scc.GetSCCWithText(scc: sccText);
                    if (sccObj == null)
                    {
                        model.Message = "Rezervasyon var fakat geçerli SCC yok";
                        return model;
                    }
                    res.sccText = sccText;
                    //SCC kodu ile uygun kapı var mı kontrol edilecek
                    //Kapı get et

                    try
                    {

                        Door myDoor = door.GetDoorAvaliableWithSCC(sccObj);
                        if (myDoor != null)
                        {
                            if (res.doorNumber != null)
                            {
                                model.Message = "Atanmış bir kapı bulunmaktadır";
                                return model;
                            }

                            model.Id = myDoor.doorNumber;
                            model.Flag = true;
                            model.Message = "*" + res.carPlate.ToUpper() + "- KAPI" + (myDoor.doorNumber + 1) + "#";

                            //    model.Message = "Rezervasyon var. Scc text:" + sccText + " verilebilecek ilk kapı : " + myDoor.doorNumber;

                            //Door state changes
                            myDoor.reservationId = res.id;
                            myDoor.lastOwnerPlate = res.carPlate;
                            myDoor.lastOwnerReservationId = res.id;
                            myDoor.dateLastOwned = DateTime.Now;
                            myDoor.order = 'R';

                            res.doorNumber = myDoor.doorNumber;
                            res.dateDoorAssigned = DateTime.Now;
                            res.dateUpdated = DateTime.Now;


                            DoorAssign assign = new DoorAssign
                            {
                                dateAssign = DateTime.Now,
                                doorNo = myDoor.doorNumber,
                                plate = res.carPlate,
                                reservationId = res.id,
                            };
                            db.DoorAssigns.Add(assign);


                            db.SaveChanges();
                        }
                        else
                        {
                            if (res.isWaiting == false)
                            {
                                // Beklemede degil ise ve uygun kapı yok ise beklemeye alınacak
                                WaitingQueue waiting = new WaitingQueue
                                {
                                    dateCreated = DateTime.Now,
                                    reservation = res,
                                    reservationId = res.id,

                                };
                                db.WaitingQueues.Add(waiting);
                                res.isWaiting = true;
                                db.SaveChanges();

                                model.Message = "*BEKLEME-ALANI#";
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
                    model.Message = "* BUTONA- BASINIZ#";
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