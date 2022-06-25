using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using thy_arac_takip_sistemi_api.Interfaces;
using thy_arac_takip_sistemi_api.Repo;

namespace thy_arac_takip_sistemi_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //! migrate!
            services.AddControllers().AddNewtonsoftJson(options =>
           options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
       );
            string conn = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IReservation, ReservationRepo>();
            services.AddTransient<IReaderModule, ReaderRepo>();
            services.AddTransient<ISCC, SCCRepo>();
            services.AddTransient<IPTS, PTSRepo>();
            services.AddTransient<IPTSLog, PTSLogRepo>();
            services.AddTransient<IDoor, DoorRepo>();
            services.AddTransient<IButtonEntry, ButtonEntryRepo>();
            services.AddTransient<ILat, LatSCCRepo>();
            services.AddTransient<ILogs, LogRepo>();
            services.AddTransient<IWaiting, WaitingRepo>();
            services.AddTransient<IConfig, ConfigRepo>();
            services.AddTransient<IUser, UserRepo>();
            services.AddTransient<IAWB, AWBRepo>();

            services.AddLogging();

            services.AddCors(options =>
                   {
                       options.AddPolicy("test",
                                         builder =>
                                         {
                                             builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                         });
                   });

            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", info: new Microsoft.OpenApi.Models.OpenApiInfo
               {
                   Version = "v1",
                   Title = "Test API",
                   Description = "ASP.NET Core Web API"
               });
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //! Migrate
            db.Database.Migrate();


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });

            app.UseCors("test");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
