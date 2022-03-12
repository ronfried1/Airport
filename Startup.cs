using FlightControlServer.AirportManager;
using FlightControlServer.ControlTowerLogic;
using FlightControlServer.Hubs;
using FlightControlServer.PlanesLogicFolder;
using FlightControlServer.TrackLogicFolder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlServer
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
            services.AddCors(options => options.AddDefaultPolicy(builder => builder.WithOrigins(
                "http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

            services.AddControllers();
            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });
            services.AddSingleton<SignalrHubs>();
            services.AddSingleton<IAppStarter,AppStarter>();
            services.AddSingleton<IControlTower,ControlTower>();
            services.AddSingleton<ITrackLogic,TrackLogic>();
            services.AddSingleton<IAirplanesLogic,AirplanesLogic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalrHubs>("/SignalrHubs");
            });
        }
    }
}
