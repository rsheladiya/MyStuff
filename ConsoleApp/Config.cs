using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp
{
 public class Config
    {
        public string site{ get; set; }
        public OutputSettings output{ get; set; }
        public IConfigurationRoot configurationRoot { get; set; }
        public Config(string[] args)
        {
            var inMemory = new Dictionary<string, string>
            {
                {"site","https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps" },
                {"output:folder","Reports"}
                //,{"output:file","report.txt"}
            };
            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemory)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("checksettings.json", true)
                .AddJsonFile("Config.json", true)
                .AddCommandLine(args)
                .AddEnvironmentVariables();

            var configuration = configBuilder.Build();
            configurationRoot = configuration;
            site = configuration["site"];
            output = configuration.GetSection("output").Get<OutputSettings>();
        }
    }
}
