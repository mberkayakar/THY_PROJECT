using System;
using System.Linq;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Interfaces
{

        public interface ILogs
        {
            IQueryable<NLogItem> GetLogs { get; }
        }
}
