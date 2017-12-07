using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var outputFolder = "Reports";
            var outputFile = "Report.text";
            //var outputPath = $"{currentDirectory}/{outputFolder}/{outputFile}";
            var outputPath = Path.Combine(currentDirectory, outputFolder, outputFile);
            //var file = Path.GetTempFileName();
            var directory = Path.GetDirectoryName(outputPath);
            Console.WriteLine(directory);
            Directory.CreateDirectory(directory);
            Console.WriteLine($"Saving report to {outputPath}.");            
            var site = "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps";
            var clinet = new HttpClient();
            var body = clinet.GetStringAsync(site);
            Console.WriteLine(body.Result);
            Console.WriteLine();
            Console.WriteLine("Links ");
            var links = LinkChecker.GetLinks(body.Result);
            links.ToList().ForEach(Console.WriteLine);
            //write to file 
            //File.WriteAllLines(outputPath, links);
           var checkresult =  LinkChecker.CheckLinks(links);
            using (var file = File.CreateText(outputPath))
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
