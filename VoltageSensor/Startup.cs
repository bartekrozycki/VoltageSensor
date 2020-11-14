using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using VoltageSensor.Models.VoltageSensor;
using VoltageSensor.Services;

namespace VoltageSensor
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
            services.Configure<SensorDatabaseSettings>
                    ( Configuration.GetSection(nameof(SensorDatabaseSettings)) );

            services.AddSingleton<ISensorDatabaseSettings>
                    ( sp => sp.GetRequiredService<IOptions<SensorDatabaseSettings>>().Value );

            services.AddSingleton<SensorService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SensorService _sensor)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.Use(async(context, next) => await new WebSocketService(_sensor).ProcessWebsocketSession(context, next));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
