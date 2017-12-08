using System;
using System.Linq;
using System.Net.Http;
using System.IO;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    public class CheckLinkJob
    {
        public void Execute(string site, OutputSettings output)
        {
            var logger = Logs.Factory.CreateLogger<CheckLinkJob>();
            
            Console.WriteLine(output.GetReportDirecory());
            Directory.CreateDirectory(output.GetReportDirecory());
            //Console.WriteLine($"Saving report to {conf.output.GetReportFilePath()}.");
            logger.LogInformation(200, $"Saving Report to {output.GetReportFilePath()}");
            //var site = "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps";
            var clinet = new HttpClient();
            var body = clinet.GetStringAsync(site);
            //Console.WriteLine(body.Result);
            logger.LogDebug(body.Result);
            //Console.WriteLine("Links ");
            var links = LinkChecker.GetLinks(site, body.Result);
            //links.ToList().ForEach(Console.WriteLine);
            //write to file 
            //File.WriteAllLines(outputPath, links);
            var checkresult = LinkChecker.CheckLinks(links);
            using (var file = File.CreateText(output.GetReportFilePath()))
            using (var linksDb = new LinksDb())
            {
                foreach (var item in checkresult.OrderBy(i => i.Exists))
                {
                    var status = item.Ismissing ? "Missing" : "Ok";
                    file.WriteLine($"{status} - {item.Link}");
                    linksDb.Links.Add(item);
                }
                linksDb.SaveChanges();
            }
            Console.ReadLine();
        }
    }
}
