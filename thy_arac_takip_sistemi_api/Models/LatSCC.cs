using System;
namespace thy_arac_takip_sistemi_api.Models
{
    public class LatSCC
    {

        public int id { get; set; }
        public DateTime? lat { get; set; }


        public int sccId { get; set; }
        public SCC scc { get; set; }
    }
}
