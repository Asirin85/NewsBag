using SQLite;
using System;

namespace NewsBag.Models
{
    public class NewsItem
    {
        
        private string id;
        [PrimaryKey]
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    id = Guid.NewGuid().ToString();
                }
                else
                    id = value;
            }
        }
        private string source;
        public string Source
        {
            get
            {
                return source;
            }
            set
            {
                if (value == null)
                {
                    source = "";
                }
                else
                    source = value;
            }
        }
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    title = "No title";
                }
                else
                    title = value;
            }
        }
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value == null)
                {
                    description = "";
                }
                else
                    description = value;
            }
        }
        public DateTimeOffset Date { get; set; }
        public string Link { get; set; }
        private string imageLink;
        public string ImageLink
        {
            get
            {
                return imageLink;
            }
            set
            {
                if (value == null)
                {
                    imageLink = "";
                }
                else
                    imageLink = value;
            }
        }
        private int imageExist;
        public int ImageExist
        {
            get
            {
                return imageExist;
            }
            set
            {
                if (value != 1 || value != 2)
                {
                    imageExist = ImageLink.Length > 0 ? 1 : 2;
                }
                else
                    imageExist = value;
            }
        }
        public NewsItem()
        {
            ID = Guid.NewGuid().ToString();
            Description = "";
            Title = "No title";
            ImageLink = "";
            ImageExist = 2;
            Source = "";
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
