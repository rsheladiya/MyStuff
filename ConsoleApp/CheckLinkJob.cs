using System;
using System.Linq;
using System.Net.Http;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleApp
{
    public class CheckLinkJob
    {
        private ILogger _Logger;
        private OutputSettings _OutputOptions;
        private SiteSettings _SiteOptions;
        private LinkChecker _LinkChecker;
        private LinksDb _LinksDb;
        public CheckLinkJob(ILogger<CheckLinkJob> logger,IOptions<OutputSettings> outputOptions,
            IOptions<SiteSettings> siteOptions,LinkChecker linkChecker,LinksDb linksDb)
        {
            _Logger = logger;
            _OutputOptions = outputOptions.Value;
            _Logger.LogInformation($"{Guid.NewGuid()}");
            _SiteOptions = siteOptions.Value;
            _LinkChecker = linkChecker;
            _LinksDb = linksDb;


        }
        public void Execute()
        {                       
            Console.WriteLine(_OutputOptions.GetReportDirecory());
            Directory.CreateDirectory(_OutputOptions.GetReportDirecory());
            //Console.WriteLine($"Saving report to {conf.output.GetReportFilePath()}.");
            _Logger.LogInformation(200, $"Saving Report to {_OutputOptions.GetReportFilePath()}");
            //var site = "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps";
            var clinet = new HttpClient();
            var body = clinet.GetStringAsync(_SiteOptions.Site);
            //Console.WriteLine(body.Result);
            _Logger.LogDebug(body.Result);
            //Console.WriteLine("Links ");
            var links = _LinkChecker.GetLinks(_SiteOptions.Site, body.Result);
            //links.ToList().ForEach(Console.WriteLine);
            //write to file 
            //File.WriteAllLines(outputPath, links);
            var checkresult = _LinkChecker.CheckLinks(links);
            using (var file = File.CreateText(_OutputOptions.GetReportFilePath()))
            //using (var linksDb = new LinksDb())
            {
                foreach (var item in checkresult.OrderBy(i => i.Exists))
                {
                    var status = item.Ismissing ? "Missing" : "Ok";
                    file.WriteLine($"{status} - {item.Link}");
                    _LinksDb.Links.Add(item);
                }
                _LinksDb.SaveChanges();
            }
            Console.ReadLine();
        }
    }
}
