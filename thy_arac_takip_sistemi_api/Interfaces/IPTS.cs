using System;
using Microsoft.AspNetCore.Http;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Interfaces
{
    public interface IPTS
    {
        //Get reader with id 
        POJO CheckReservationPTS(PTS pts,IFormFile file);
        POJO CheckReservationPTSWithoutPTSLOG(PTS pts,IFormFile file);

    }
}
