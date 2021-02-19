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
        public void ConfigureServices(IServiceCollection services) {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello, world");
            });
        }

    }

}

