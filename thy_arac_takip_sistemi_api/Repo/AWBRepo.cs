using System;
using System.Linq;
using System.Linq.Expressions;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class AWBRepo : IAWB
    {
        private readonly MyDbContext _db;
        public AWBRepo(MyDbContext db)
        {
                _db = db;
        }

        public Config GetConfigValueWithKey(Expression<Func<Config, bool>> where = null)
        {
            if (where != null)
            {
                return _db.Configs.FirstOrDefault(where);
            }
            return _db.Configs.FirstOrDefault(where);
        }
    }
}
