using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using thy_arac_takip_sistemi_api.Models;


namespace thy_arac_takip_sistemi_api.Interfaces
{
    public interface IConfig
    {

        IQueryable<Config> GetConfigs { get; }
        POJO UpdateConfig(Config config);
    }
}
