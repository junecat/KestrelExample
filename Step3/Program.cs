using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace ServerApp {
    public class Program {
        public static void Main(string[] args) {

            var host = new WebHostBuilder().UseKestrel(GetKso()).UseContentRoot(Directory.GetCurrentDirectory()).UseStartup<Startup>().UseWebRoot("static").Build();
            host.Run();

        }

        static Action<KestrelServerOptions> GetKso() {
            Action<KestrelServerOptions> ret = options =>
            {
                options.Limits.MaxConcurrentConnections = 100;
                options.Listen(IPAddress.Any, 5000);
            };
            return ret;
        }


    }

    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
 
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.Run(async (context) => {
                await context.Response.WriteAsync("Hello, world");
            });
        }

    }

}

