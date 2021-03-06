using NewsBag.Models;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace NewsBag.Services
{
    public class OneParser
    {
        public static async Task GetNews(ObservableCollection<NewsItem> list, string source, Stream xmlStream)
        {
            using (XmlTextReader reader = new XmlTextReader(xmlStream))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType is XmlNodeType.Element && reader.Name.Equals("item"))
                    {
                        var item = new NewsItem();
                        item.ID = Guid.NewGuid().ToString();
                        item.Source = source;
                        while (reader.Read() && !(reader.NodeType == XmlNodeType.EndElement && reader.Name.Equals("item")))
                        {
                            switch (reader.Name)
                            {
                                case "title":
                                    if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                                    {
                                        item.Title = reader.Value;
                                    }
                                    break;
                                case "link":
                                    if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                                    {
                                        item.Link = reader.Value;
                                    }
                                    break;
                                case "rbc_news:full-text":
                                    if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                                    {
                                        item.Description = reader.Value;
                                    }
                                    break;
                                case "description":
                                    if (reader.Read() && (reader.NodeType is XmlNodeType.Text || reader.NodeType is XmlNodeType.CDATA))
                                    {
                                        item.Description = reader.Value;
                                        if (reader.NodeType is XmlNodeType.CDATA) while (reader.Read() && reader.NodeType != XmlNodeType.EndElement) ;
                                    }
                                    else if (source.Equals("lenta.ru") && reader.Read() && reader.NodeType is XmlNodeType.CDATA)
                                    {
                                        item.Description = reader.Value;
                                        while (reader.Read() && reader.NodeType != XmlNodeType.EndElement) ;
                                    }
                                    break;
                                case "pubDate":
                                    if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                                    {
                                        item.Date = DateTimeOffset.Parse(reader.Value, CultureInfo.InvariantCulture).LocalDateTime;
                                    }
                                    break;
                                case "guid":
                                    if (reader.Read() && reader.NodeType is XmlNodeType.Text)
                                    {
                                        item.ID = reader.Value;
                                        if (list.Any(x => x.ID.Equals(item.ID))) while (reader.Read() && !(reader.NodeType == XmlNodeType.EndElement && reader.Name.Equals("item"))) ;
                                    }
                                    break;
                                case "enclosure":
                                    if (reader.MoveToNextAttribute() && reader.Name.Equals("url"))
                                    {
                                        item.ImageLink = reader.Value;
                                        item.ImageExist = 1;
                                    }
                                    break;
                            }
                        }
                        list.Add(item);

                    }

                }
            }

            return;
        }
    }
}
