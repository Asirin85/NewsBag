using NewsBag.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Xamarin.Forms;

namespace NewsBag.Models
{
    public class GlobalNewsConstants
    {
        public static string filter = "";
        public static readonly XmlGetter xmlGetter = new XmlGetter();
        public static NewsItem SelectedItem;
        public static Tab TabNews;
        public static List<ShellContent> TabNewsItems = new List<ShellContent>();


        public static readonly HttpClient httpClient = new HttpClient();
        public static readonly string apiLogin = "http://192.168.253.2:8090/api/login";
        public static readonly string apiRegister = "http://192.168.253.2:8090/api/registration";
        public static Dictionary<string, string> sourcesLinks = new Dictionary<string, string>()
        {
            {"rbc.ru",  $"http://static.feed.rbc.ru/rbc/logical/footer/news.rss" },
            {"lenta.ru",$"https://lenta.ru/rss/news" },
            {"un.org",$"https://news.un.org/feed/subscribe/en/news/region/europe/feed/rss.xml" },

        };


        public const string DatabaseFilename = "NewsSQLite.db3";
        public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
