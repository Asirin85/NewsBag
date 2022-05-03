using NewsBag.Services.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsBag.Models
{
    public class GlobalNewsFilter
    {
       public static string filter = "";
       public static OneParser parser = new OneParser();
        public static Dictionary<string, string> sourcesLinks = new Dictionary<string, string>()
        {
            {"rbc.ru",  $"http://static.feed.rbc.ru/rbc/logical/footer/news.rss" },
            {"lenta.ru",$"https://lenta.ru/rss/news" },
            {"un.org",$"https://news.un.org/feed/subscribe/en/news/region/europe/feed/rss.xml" },

        };
    }
}
