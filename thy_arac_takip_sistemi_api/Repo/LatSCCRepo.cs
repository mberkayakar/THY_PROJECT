using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class LatSCCRepo : ILat
    {
        private readonly MyDbContext db;
        private readonly ISCC scc;

        public LatSCCRepo(MyDbContext _db, ISCC sc)
        {
            db = _db;
            scc = sc;
        }


        LatSCC ILat.GetLatSCC(string sccText)
        {
            try
            {
                SCC sccObj = scc.GetSCCWithText(sccText);
                if (sccObj != null)
                {
                    LatSCC lat
                   = db.Lats.Include(a=>a.scc).FirstOrDefault(e => e.scc == sccObj);
                    return lat;

                }
                else{
                    return new LatSCC { lat = null };

                }


            }
            catch (Exception ex)
            {
                return new LatSCC {lat=null };
            }

        }

        POJO ILat.PostLatSCC(LatSCC latScc)
        {

            POJO model = new POJO { Flag = false };
            try
            {
                db.Lats.Add(latScc);
                db.SaveChanges();
                model.Flag = true;
                model.Message = "Lat bilgisi oluşturuldu";
            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();

            }
            return model;
        }
    }
}
