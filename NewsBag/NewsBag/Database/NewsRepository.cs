using NewsBag.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsBag.Database
{
    public class NewsRepository : IDataRepo<NewsItem>
    {
        private readonly SQLiteAsyncConnection _database;
        public NewsRepository(SQLiteAsyncConnection database)
        {
            _database = database;
        }
        public Task<int> AddItemAsync(NewsItem item)
        {
            if (string.IsNullOrEmpty(item.ID)) throw new ArgumentNullException(nameof(item));
            return _database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(NewsItem item)
        {
            return _database.DeleteAsync(item);
        }

        public async Task<bool> ItemExists(string id)
        {
            return await _database.Table<NewsItem>().Where(i => i.ID == id).FirstOrDefaultAsync() != null;
        }

        public Task<List<NewsItem>> GetItemsAsync()
        {
            return _database.Table<NewsItem>().ToListAsync();
        }
    }
}
