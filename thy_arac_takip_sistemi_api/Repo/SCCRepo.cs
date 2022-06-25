using System;
using System.Linq;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class SCCRepo : ISCC
    {

        private readonly MyDbContext db;
        public SCCRepo(MyDbContext _db)
        {
            db = _db;
        }

        IQueryable<SCC> ISCC.GetAllSCC => db.SCCs;

        public POJO DeleteSCC(int? id)
        {
            POJO model = new POJO { Flag = false };

            try
            {
                SCC scc = db.SCCs.Where((e) => e.id == id).FirstOrDefault();
                if (scc == null)
                {
                    model.Message = "Böyle bir SCC bulunamadı";
                    model.Id = id;
                }
                db.SCCs.Remove(scc);
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

        POJO ISCC.CreateSCC(SCC scc)
        {
            POJO model = new POJO
            {
                Flag = false
            };
            try
            {
                //Check scc is exist ?
                if(db.SCCs.FirstOrDefault(e=>e.code==scc.code)!=null){
                    model.Flag = false;
                    model.Message = "SCC is exist";
                    return model;
                }
                db.SCCs.Add(scc);
                db.SaveChanges();
                model.Id = scc.id;
                model.Flag = true;
                model.Message = "SCC created : ->" + scc.code;
            }
            catch (Exception ex)
            {
                model.Message = ex.ToString();
            }
            return
                model;
        }
        SCC ISCC.GetSCCWithText(string scc)
        {
            SCC model = db.SCCs.Where((e) => e.code == scc).FirstOrDefault();
            return model;
        }

       POJO ISCC.ModifySCC(SCC _scc)
        {
            POJO model = new POJO { Flag = false };
            SCC scc = db.SCCs.Where((e) => e.id == _scc.id).FirstOrDefault();

            try
            {
                if (scc != null)
                {
                    db.Entry(scc).CurrentValues.SetValues(_scc);
                    db.SaveChanges();
                    model.Flag = true;
                    model.Message = "Başarıyla Güncellendi";
                    model.Id = scc.id;
                }
                else
                {
                    model.Message = "Böyle bir scc bulunamadı";

                }

            }
            catch (Exception e)
            {
                model.Message = e.ToString();


            }
            return model;
        }
    }
}
