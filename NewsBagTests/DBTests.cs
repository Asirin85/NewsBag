using Moq;
using NewsBag.Database;
using NewsBag.Models;
using NUnit.Framework;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
namespace NewsBagTests
{
    public class DBTests
    {
        private static SQLiteAsyncConnection _databaseConnection;
        [SetUp]
        public async Task Setup()
        {
            _databaseConnection = new SQLiteAsyncConnection(":memory:");
            await _databaseConnection.CreateTableAsync<NewsItem>();
        }
        [TearDown]
        public async Task TearDown()
        {
            if (_databaseConnection != null)
                await _databaseConnection.CloseAsync();
        }
        [Test]
        public async Task TestForDatabaseRepositoryAddItem()
        {
            var repo = new NewsRepository(_databaseConnection);
            var date = DateTimeOffset.Parse("Fri, 06 May 2022 20:55:44 +0300", CultureInfo.InvariantCulture).LocalDateTime;
            var item = new NewsItem("ID", "source", "title", "description", date, "link", "imagelink", 1);
            await repo.AddItemAsync(item);
            var retrived = await _databaseConnection.GetAsync<NewsItem>(item.ID);
            // Assert
            retrived.Should().BeEquivalentTo(item);

        }
        [Test]
        public async Task TestForDatabaseRepositoryItemExist()
        {
            var repo = new NewsRepository(_databaseConnection);
            var id = "ID";
            var exists = await repo.ItemExists(id);
            // Assert
            Assert.AreEqual(false, exists);
            var date = DateTimeOffset.Parse("Fri, 06 May 2022 20:55:44 +0300", CultureInfo.InvariantCulture).LocalDateTime;
            var item = new NewsItem(id, "source", "title", "description", date, "link", "imagelink", 1);
            await _databaseConnection.InsertAsync(item);
            exists = await repo.ItemExists(id);
            // Assert
            Assert.AreEqual(true, exists);
        }
        [Test]
        public async Task TestForDatabaseRepositoryGetItems()
        {
            var repo = new NewsRepository(_databaseConnection);
            var list = new List<NewsItem>();
            var date = DateTimeOffset.Parse("Fri, 06 May 2022 20:55:44 +0300", CultureInfo.InvariantCulture).LocalDateTime;
            for (int i = 0; i < 10; i++)
            {
                list.Add(new NewsItem(i.ToString(), "source", "title", "description", date, "link", "imagelink", 1));
            }
            foreach (var item in list)
            {
                await _databaseConnection.InsertAsync(item);
            }
            var items = await repo.GetItemsAsync();
            // Assert
            Assert.AreEqual(list.Count, items.Count);
            list.Should().BeEquivalentTo(items);
        }
        [Test]
        public async Task TestForDatabaseRepositoryDeleteItem()
        {
            var repo = new NewsRepository(_databaseConnection);
            var list = new List<NewsItem>();
            var date = DateTimeOffset.Parse("Fri, 06 May 2022 20:55:44 +0300", CultureInfo.InvariantCulture).LocalDateTime;
            var id = "ID";
            var item = new NewsItem(id, "source", "title", "description", date, "link", "imagelink", 1);
            await _databaseConnection.InsertAsync(item);
            var itemFromBase = await _databaseConnection.GetAsync<NewsItem>(id);
            // Assert
            itemFromBase.Should().BeEquivalentTo(item);
            await repo.DeleteItemAsync(item);
            // Assert 
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _databaseConnection.GetAsync<NewsItem>(id));
        }
    }
}
