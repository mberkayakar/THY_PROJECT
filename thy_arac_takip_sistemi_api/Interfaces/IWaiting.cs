using System;
using System.Collections.Generic;
using System.Linq;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Interfaces
{

    public interface IWaiting
    {
        IQueryable<WaitingQueue> GetWaitingQuery { get; }
        POJO DeleteWaiting(int id);
        POJO DeleteWaitings(List<int> idList);
    }
}
