using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    class Program
    {
      static void Main(string[] args)
        {
            Config conf = new Config(args);
            Logs.Init(conf.configurationRoot);
            var logger =Logs.Factory.CreateLogger<Program>();
            
            //var currentDirectory = Directory.GetCurrentDirectory();
            //var outputSettings = new OutputSettings();
            //configuration.GetSection("output").Bind(outputSettings);

            //var outputPath = outputSettings.GetOutputPath();
            //var outputFolder = outputSettings.Folder;
            //var outputFile = outputSettings.File;
            //var outputFolder = configuration["output:folder"];
            //var outputFile = configuration["output:file"];

            //var outputFolder = "Reports";
            //var outputFile = "Report.text";
            //var outputPath = $"{currentDirectory}/{outputFolder}/{outputFile}";
            //var outputPath = Path.Combine(currentDirectory, outputSettings.Folder, outputSettings.File);
            //var file = Path.GetTempFileName();
            //var directory = Path.GetDirectoryName(outputPath);
            //var directory = Path.GetDirectoryName(outputSettings.GetOutputPath());

            //var directory = outputSettings.GetReportDirecory();
            Console.WriteLine(conf.output.GetReportDirecory());
            Directory.CreateDirectory(conf.output.GetReportDirecory());
            //Console.WriteLine($"Saving report to {conf.output.GetReportFilePath()}.");
            logger.LogInformation(200,$"Saving Report to {conf.output.GetReportFilePath()}");
            //var site = "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps";
            var clinet = new HttpClient();
            var body = clinet.GetStringAsync(conf.site);
            //Console.WriteLine(body.Result);
            logger.LogDebug(body.Result);
            //Console.WriteLine("Links ");
            var links = LinkChecker.GetLinks(conf.site,body.Result);            
            //links.ToList().ForEach(Console.WriteLine);
            //write to file 
            //File.WriteAllLines(outputPath, links);
           var checkresult =  LinkChecker.CheckLinks(links);
            using (var file = File.CreateText(conf.output.GetReportFilePath()))
            {
                foreach (var item in checkresult.OrderBy(i=>i.Exists))
                {
                    var status = item.Ismissing ? "Missing" : "Ok";
                    file.WriteLine($"{status} - {item.Link}");

                }
            }
            Console.ReadLine();

        }
    }
}
