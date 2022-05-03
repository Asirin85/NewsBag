using NewsBag.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NewsBag.Services.Parsers
{
    public class LentaParser
    {
        public static async Task<List<NewsItem>> GetNews()
        {
            var list = new List<NewsItem>();
            using (XmlTextReader reader = new XmlTextReader($"https://lenta.ru/rss/news"))
            {
                while (reader.Read())
                {
                    if (reader.NodeType is XmlNodeType.Element && reader.Name.Equals("item"))
                    {
                        var item = new NewsItem();
                        item.ID = Guid.NewGuid();
                        item.Source = "lenta.ru";
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
                        while (reader.Read() && !reader.Name.Equals("description")) ;
                        if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                        {
                            item.Description = reader.Value;
                        }
                        while (reader.Read() && !reader.Name.Equals("pubDate")) ;
                        if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                        {
                            item.Date = DateTimeOffset.Parse(reader.Value, CultureInfo.InvariantCulture).LocalDateTime;
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
            return list;

        }
    }
}
