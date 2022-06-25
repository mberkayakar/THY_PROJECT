using System;
using System.Collections.Generic;
using System.Linq;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Interfaces
{
    public interface IPTSLog
    {
        IQueryable<PTS> GetPlateEntries { get; }
        POJO DeletePlateEntry(int id);
        POJO DeletePlateEntries(List<int> idList);
        PTS UpdatePlate(PTS pts);

    }
}
