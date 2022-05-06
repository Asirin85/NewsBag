using SQLite;
using System;

namespace NewsBag.Models
{
    public class NewsItem
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Link { get; set; }
        public string ImageLink
        {
            get; set;
        }
        public int ImageExist { get; set; } = 2;
        public NewsItem()
        {

        }
        public NewsItem(string iD, string source, string title, string description, DateTimeOffset date, string link, string imageLink, int imageExist)
        {
            ID = iD;
            Source = source;
            Title = title;
            Description = description;
            Date = date;
            Link = link;
            ImageLink = imageLink;
            ImageExist = imageExist;
        }
    }
}
