using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using thy_arac_takip_sistemi_api.Models;


namespace thy_arac_takip_sistemi_api.Interfaces
{
    public interface IButtonEntry
    {

        POJO AddButtonEntry(ButtonEntry button,IFormFile file);
        IQueryable<ButtonEntry> GetButtonEntries{ get; }
        POJO DeleteButtonEntry(int id);
        POJO DeleteButtonEntries(List<int> idList);
        ButtonEntry UpdateButtonEntry(ButtonEntry pts);
    }
}
