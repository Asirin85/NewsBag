using NewsBag.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NewsBag.Services.Parsers
{
    public static class UnParser
    {
        public static async Task<List<NewsItem>> GetNews()
        {
            var list = new List<NewsItem>();
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("user-agent", "MyRSSReader/1.0");
                using (XmlTextReader reader = new XmlTextReader(webClient.OpenRead($"https://news.un.org/feed/subscribe/en/news/region/europe/feed/rss.xml")))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType is XmlNodeType.Element && reader.Name.Equals("item"))
                        {
                            var item = new NewsItem();
                            item.ID = Guid.NewGuid();
                            item.Source = "news.un.org";
                            while (reader.Read() && !reader.Name.Equals("title") && !reader.Name.Equals("item")) ;
                            if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                            {
                                item.Title = reader.Value;
                            }
                            while (reader.Read() && !reader.Name.Equals("link") && !reader.Name.Equals("item")) ;
                            if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                            {
                                item.Link = reader.Value;
                            }
                            while (reader.Read() && !reader.Name.Equals("description") && !reader.Name.Equals("item")) ;
                            if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                            {
                                item.Description = reader.Value;
                            }
                            while (reader.Read() && !reader.Name.Equals("enclosure")) ;
                            if (reader.MoveToNextAttribute() && reader.Name.Equals("url"))
                            {
                                item.ImageLink = reader.Value;
                                item.ImageExist = 1;
                            }
                            while (reader.Read() && !reader.Name.Equals("pubDate") && !reader.Name.Equals("item")) ;
                            if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                            {
                                item.Date = DateTimeOffset.Parse(reader.Value, CultureInfo.InvariantCulture).LocalDateTime;
                            }
                            list.Add(item);
                        }
                    }

                }
            }
            return list;
        }
    }
}
