using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
   public static class Logs
    {
        public static LoggerFactory Factory = new LoggerFactory(); 
        public static void Init(IConfiguration configuration)
        {
          
            //Factory.AddConsole(LogLevel.Trace,includeScopes:true);
            Factory.AddConsole(configuration.GetSection("Logging"));
            //factory.AddDebug(LogLevel.Debug);
            Factory.AddFile("logs/checklinks-{Date}.json",
                isJson:true,
                minimumLevel:LogLevel.Trace);
        }

    }
}
