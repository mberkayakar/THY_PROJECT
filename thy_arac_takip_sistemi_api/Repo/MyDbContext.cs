using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thy_arac_takip_sistemi_api.Models;

namespace thy_arac_takip_sistemi_api.Repo
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Config>().HasData(
                new Config { key = "RESERVATION_DELAY", value = "300" },
                new Config { key = "BUTTON_DELAY", value = "7200" },
                new Config { key = "SMS_URL", value = "https://wsdev.thy.com/sms-gateway/send-sms/bulk" },
                new Config { key = "SMS_USERNAME", value = "AYP-THYAO" },
                new Config { key = "SMS_PASSWORD", value = "hPhjix8sOvf2" },
                new Config { key = "SMS_BASIC_AUTH_USERNAME", value = "wstestuser" },
                new Config { key = "SMS_BASIC_AUTH_PASSWORD", value = "thy1234" },

                // AWB sistemine atılan awb isteği için eklenen Seed DAta
                new Config { key = "apikey", value = "l7xx890646a1315c4c4181441a0c292a4666" },
                new Config { key = "apisecret", value = "3f0517bbf36441f88488a86703d4acb5" },
                new Config { key = "clientTransactionId", value = "TEST1234" }

 
 
            //ADD sms messages

            );
            modelBuilder.Entity<SCC>().HasData(
                new SCC { id = 1, code = "JOKER", strength = 1 },
                new SCC { id = 2, code = "IMPORT", strength = 2 },
                new SCC { id = 3, code = "ICHAT", strength = 2 }
            );

        }


        public DbSet<NLogItem> NLogs { get; set; }



        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AWB> AWBs { get; set; }
        public DbSet<SCCText> SCCTexts { get; set; }

        public DbSet<ReaderModule> ReaderModules { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<SCC> SCCs { get; set; }



        public DbSet<WaitingQueue> WaitingQueues { get; set; }
        public DbSet<ButtonEntry> ButtonEntries { get; set; }

        public DbSet<Config> Configs { get; set; }


    //LOG
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<PTS> PTSLogs { get; set; }
        public DbSet<EventLog> EventLogs{ get; set; }


        public DbSet<User> Users { get; set; }

        public DbSet<LatSCC> Lats { get; set; }

        public DbSet<DoorAssign> DoorAssigns { get; set; }

    

    }
}