using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace LostArkStatus
{
    public static class ServerFetcher
    {
        public static  List<Server> GetServerList()
        {
            using var client = new WebClient();
            var resp =  client.DownloadString("https://www.playlostark.com/en-gb/support/server-status");
            
            var htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(resp);

            var servers = (from link in htmlSnippet.DocumentNode.SelectNodes("//div[@class]") select link.Attributes["class"] into att where att.Value == "ags-ServerStatus-content-responses-response-server" select new Server { Status = GetStatus(att.OwnerNode.ChildNodes.First(c => c.Name == "div" && c.GetAttributeValue("class", "") == "ags-ServerStatus-content-responses-response-server-status-wrapper").ChildNodes.First(c => c.GetAttributeValue("class", "").Contains("ags-ServerStatus-content-responses-response-server-status--")).GetAttributeValue("class", "")), Name = att.OwnerNode.ChildNodes.First(c => c.Name == "div" && c.GetAttributeValue("class", "") == "ags-ServerStatus-content-responses-response-server-name").ChildNodes.First().InnerHtml.Replace("\n", "").Trim() }).ToList();

            return servers;
        }
        private static string GetStatus(string status)
        {
            return status[(status.LastIndexOf("--", StringComparison.Ordinal) + 2)..];
        }

    }
    
  
    public class Server
    {
        public string Name;
        public string Status;
    }
}