using NewsBag.Models;
using NewsBag.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NewsBagTests
{
    public class Tests
    {
        private OneParser _parser;
        [OneTimeSetUp]
        public void Setup()
        {
            _parser = new OneParser();
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            if (_parser != null)
            {
                _parser.Dispose();
                _parser = null;
            }
        }
        private static readonly object[] parserData = 
        {
            new object[]{"lenta.ru","https://lenta.ru/rss/news" },
            new object[]{"un.org","https://news.un.org/feed/subscribe/en/news/region/europe/feed/rss.xml" },
            new object[]{"rbc.ru","http://static.feed.rbc.ru/rbc/logical/footer/news.rss" }
        };

        [TestCaseSource(nameof(parserData))]
        public async Task TestOnCorrectParsing(string source, string data)
        {
            var result = new ObservableCollection<NewsItem>();
            await _parser.GetNews(result, source, data);
            Assert.That(result.Count > 0);
            var allTitlesAndDescriptions = true;
            var index = 0;
            while (allTitlesAndDescriptions && index < result.Count)
            {
                if (string.IsNullOrEmpty(result[index].Title) || string.IsNullOrEmpty(result[index].Description))
                {
                    allTitlesAndDescriptions = false;
                }
                index++;
            }
            Assert.AreEqual(allTitlesAndDescriptions, true);
        }
    }
}