using System;
using System.Linq;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Interfaces
{
    public interface ISCC
    {
        //Get reader with id 
        POJO CreateSCC(SCC scc);
        POJO DeleteSCC(int? id);
        POJO ModifySCC(SCC scc);
        IQueryable<SCC> GetAllSCC { get; }
        SCC GetSCCWithText(string scc);
    }
}
