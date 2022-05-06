using NewsBag.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsBagTests
{
    public class NewsItemsTests
    {
        [Test]
        public void TestForNewsItemsModel()
        {
            var date = DateTimeOffset.Parse("Fri, 06 May 2022 20:55:44 +0300", CultureInfo.InvariantCulture).LocalDateTime;
            var model = new NewsItem(null,null,null,null, date, null,null,0);
            Assert.That(!string.IsNullOrEmpty(model.ID));
            Assert.AreEqual("", model.Source);
            Assert.AreEqual("No title", model.Title);
            Assert.AreEqual("", model.Description);
            Assert.AreEqual("", model.ImageLink);
            Assert.AreEqual(2, model.ImageExist);
        }
    }
}
