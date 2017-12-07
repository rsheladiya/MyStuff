using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var site = "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps";
            var clinet = new HttpClient();
            var body = clinet.GetStringAsync(site);
            Console.WriteLine(body.Result);
            Console.WriteLine();
            Console.WriteLine("Links");
            var links = LinkChecker.GetLinks(body.Result);
            links.ToList().ForEach(Console.WriteLine);
            Console.ReadLine();

        }
    }
}
