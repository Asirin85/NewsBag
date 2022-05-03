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
    public class RbcParser
    {
        public static async Task<List<NewsItem>> GetNews()
        {
            var list = new List<NewsItem>();
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("user-agent", "MyRSSReader/1.0");
                using (XmlTextReader reader = new XmlTextReader(webClient.OpenRead($"http://static.feed.rbc.ru/rbc/logical/footer/news.rss")))
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        if (reader.NodeType is XmlNodeType.Element && reader.Name.Equals("item"))
                        {
                            var item = new NewsItem();
                            item.ID = Guid.NewGuid();
                            item.Source = "rbc.ru";
                            while (reader.Read() && !reader.Name.Equals("title")) ;
                            if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                            {
                                item.Title = reader.Value;
                            }
                            while (reader.Read() && !reader.Name.Equals("link")) ;
                            if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                            {
                                item.Link = reader.Value;
                            }
                            while (reader.Read() && !reader.Name.Equals("pubDate")) ;
                            if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                            {
                                item.Date = DateTimeOffset.Parse(reader.Value, CultureInfo.InvariantCulture).LocalDateTime;
                            }
                            while (reader.Read() && !reader.Name.Equals("rbc_news:full-text")) ;
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
                            list.Add(item);
                        }
                    }

                }
            }
            return list;
        }
    }
}
