using System;
using Microsoft.EntityFrameworkCore;

namespace thy_arac_takip_sistemi_api.Models
{
    // User table for logins and authority
    [Index(nameof(email), IsUnique = true)]

    public class User
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        //Title
        public string title { get; set; }
        //Role
        public string role { get; set; }
        //Yetki  110010
        //Yetki sahibinin yapabildikleri
        /// SCC Kapı Ataması                     
        /// SCC Oluşturmak
        /// Manuel Kapı Atama
        /// Manuel Kapı Düzenleme
        /// Rezervasyon Düzenleme

        public string authority { get; set; }


        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        //
        public DateTime? dateLastLogin { get; set; }
        public DateTime? dateCreated { get; set; }
        public DateTime? dateUpdated { get; set; }


    }
}
