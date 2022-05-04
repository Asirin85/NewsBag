using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

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
            get;set;
        }
        public int ImageExist { get; set; } = 2;
    }
}
