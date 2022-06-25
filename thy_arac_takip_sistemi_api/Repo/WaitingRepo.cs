using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class WaitingRepo : IWaiting
    {
        private readonly MyDbContext db;

        public WaitingRepo(MyDbContext _db)
        {
            db = _db;
        }

        public IQueryable<WaitingQueue> GetWaitingQuery => db.WaitingQueues.Include(e => e.reservation);



        public POJO DeleteWaitings(List<int> idList)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                foreach (var item in idList)
                {
                    WaitingQueue pts = db.WaitingQueues.FirstOrDefault(e => e.id == item);
                    if (pts != null)
                    {
                        db.WaitingQueues.Remove(pts);
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
        public POJO DeleteWaiting(int id)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                WaitingQueue pts = db.WaitingQueues.FirstOrDefault(e => e.id == id);
                if (pts != null)
                {
                    db.WaitingQueues.Remove(pts);
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
    }
}
