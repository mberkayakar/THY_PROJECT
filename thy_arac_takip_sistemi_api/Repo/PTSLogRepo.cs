using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class PTSLogRepo:IPTSLog
    {
        private readonly MyDbContext db;



        public PTSLogRepo(MyDbContext _db)
        {
            db = _db;

        }

         IQueryable<PTS> IPTSLog.GetPlateEntries => db.PTSLogs;

        public POJO DeletePlateEntries(List<int> idList)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                foreach (var item in idList)
                {
                    PTS pts = db.PTSLogs.FirstOrDefault(e => e.id == item);
                    if (pts != null)
                    {
                        db.PTSLogs.Remove(pts);
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

        public POJO DeletePlateEntry(int id)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                PTS pts = db.PTSLogs.FirstOrDefault(e => e.id == id);
                if(pts!=null)
                {
                    db.PTSLogs.Remove(pts);
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

        public PTS UpdatePlate(PTS pts)
        {
            try
            {
                PTS _pts = db.PTSLogs.FirstOrDefault(e => e.id == pts.id);
                if (_pts != null)
                {
                    _pts.plate = pts.plate;
                }
                db.SaveChanges();
                return _pts;
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }
    }
}


