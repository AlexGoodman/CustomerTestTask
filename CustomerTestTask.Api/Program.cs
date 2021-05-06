using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerTestTask.Api.Helpers;
using CustomerTestTask.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CustomerTestTask.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {            
            IHost host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {                            
                await DbHelper.Initialize(scope.ServiceProvider.GetRequiredService<Context>());
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
