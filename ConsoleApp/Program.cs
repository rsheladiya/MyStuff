using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    class Program
    {
      static void Main(string[] args)
        {
            //GlobalConfiguration.Configuration.UseMemoryStorage();
            var host = new WebHostBuilder()
                .UseKestrel()                       
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()                
                .Build();

            RecurringJob.AddOrUpdate<CheckLinkJob>("Check-Link",
               J => J.Execute(), Cron.Minutely);
            RecurringJob.Trigger("Check-Link");            
            
            host.Run();
           
        }
    }
}
