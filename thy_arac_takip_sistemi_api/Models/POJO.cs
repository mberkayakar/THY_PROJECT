

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace thy_arac_takip_sistemi_api.Models

{
    public class POJO
    {
        public int? Id { get; set; }
        public int? acenteId { get; set; }
        public int? NumberOfRows { get; set; }
        public bool Flag { get; set; }
        public string Message { get; set; }

        public POJO()
        {
            Flag = false;
        }
    }
}