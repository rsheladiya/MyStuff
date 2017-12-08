using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
      static void Main(string[] args)
        {
            Config conf = new Config(args);
            Logs.Init(conf.configurationRoot);

            //GlobalConfiguration.Configuration.UseMemoryStorage();
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            //RecurringJob.AddOrUpdate<CheckLinkJob>("Check-Link",J=>J.Execute(conf.site,conf.output), Cron.Minutely);
            //RecurringJob.Trigger("Check-Link");            
            RecurringJob.AddOrUpdate(() => Console.WriteLine("simple!"), Cron.Minutely);

            host.Run();
           
        }
    }
}
