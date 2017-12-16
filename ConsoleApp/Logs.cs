using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
   public static class Logs
    {
        //public static ILoggerFactory Factory = new LoggerFactory(); 
        public static void Init(ILoggerFactory factory, IConfiguration configuration)
        {
          
            //Factory.AddConsole(LogLevel.Trace,includeScopes:true);
            factory.AddConsole(configuration.GetSection("Logging"));
            //factory.AddDebug(LogLevel.Debug);
            factory.AddFile("logs/checklinks-{Date}.json",
                isJson:true,
                minimumLevel:LogLevel.Trace);
        }

    }
}
