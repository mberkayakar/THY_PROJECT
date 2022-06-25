using System;
using System.Linq;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class LogRepo : ILogs
    {
        private readonly MyDbContext db;



        public LogRepo(MyDbContext _db)
        {
            db = _db;

        }

        IQueryable<NLogItem> ILogs.GetLogs => db.NLogs.Skip(Math.Max(0, db.NLogs.Count() - 500)).OrderBy(e => e.Date);


    }
}
