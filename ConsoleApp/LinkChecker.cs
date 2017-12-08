using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace ConsoleApp
{
    public class LinkChecker
    {
        protected static readonly ILogger<LinkChecker> logger = Logs.Factory.CreateLogger<LinkChecker>();
        public static IEnumerable<string> GetLinks(string link, string page)
        {
            
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(page);
            var originalLinks = htmlDocument.DocumentNode.SelectNodes("//a[@href]")
                .Select(n => n.GetAttributeValue("href", string.Empty)).ToList();
            //var logger = Logs.Factory.CreateLogger<LinkChecker>();
            //logger.LogTrace(string.Join(",", originalLinks));htmlDocument.LoadHtml(page);
            using (logger.BeginScope($"Getting links from {link}"))
            {
                originalLinks.ForEach(i => logger.LogTrace(100,"Origional Link:{link}",i));
            }
            var links = originalLinks
                .Where(l => !String.IsNullOrEmpty(l))
                .Where(l => l.StartsWith("http"));
            return links;
        }

        public static IEnumerable<LinkCheckResult> CheckLinks(IEnumerable<string> links)
        {
            var all = Task.WhenAll(links.Select(CheckLink));
            return all.Result;

        }

        private static async Task<LinkCheckResult> CheckLink(string link)
        {
            var result = new LinkCheckResult();
            result.Link = link;
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Head, link);
                try
                {
                    var response = await client.SendAsync(request);
                    result.Problem = response.IsSuccessStatusCode ? null : response.StatusCode.ToString();
                    return result;
                }
                catch (HttpRequestException exception)
                {
                   logger.LogTrace(0, exception, "Failed to retrive {link}", link);
                    result.Problem = exception.Message;
                    return result;
                }
            }
        }
    }

    public class LinkCheckResult
        {
        public bool Exists => String.IsNullOrWhiteSpace(Problem);
        public bool Ismissing => !Exists;
        public string Problem { get; set; }
        public string Link { get; set; }
        }

}
