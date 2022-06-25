using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
namespace thy_arac_takip_sistemi_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
                 .ConfigureLogging((hostingContext, logging) =>
                 {
                     logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging")); //logging settings under appsettings.json
                     logging.AddConsole(); //Adds a console logger named 'Console' to the factory.
                     logging.AddDebug(); //Adds a debug logger named 'Debug' to the factory.
                     logging.AddEventSourceLogger(); //Adds an event logger named 'EventSource' to the factory.
                                                     // Enable NLog as one of the Logging Provider

                     logging.AddNLog();

                 })
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
                   // webBuilder.UseUrls("https://0.0.0.0:5001", "https://192.168.1.23:5001").UseStartup<Startup>();
               });
    }
}
