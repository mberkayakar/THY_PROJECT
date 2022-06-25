using System;
using System.Linq.Expressions;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Interfaces
{
    public interface IAWB
    {
        Config GetConfigValueWithKey(Expression<Func<Config,bool>> where = null);
    }
}
