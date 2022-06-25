using System;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Interfaces
{
 public interface ILat
 {
        public LatSCC GetLatSCC(string sccText);
        public POJO PostLatSCC(LatSCC latScc);


    }
}
