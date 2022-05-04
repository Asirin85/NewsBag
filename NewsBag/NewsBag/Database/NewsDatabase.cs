using NewsBag.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsBag.Database
{
    public class NewsDatabase : IDataRepo<NewsItem>
    {
        static SQLiteAsyncConnection Database;
        public static readonly AsyncLazy<NewsDatabase> Instance = new AsyncLazy<NewsDatabase>(async () =>
        {
            var instance = new NewsDatabase();
            CreateTableResult result = await Database.CreateTableAsync<NewsItem>();
            return instance;
        });

        public NewsDatabase()
        {
            Database = new SQLiteAsyncConnection(GlobalNewsConstants.DatabasePath, GlobalNewsConstants.Flags);
        }

        public Task<int> AddItemAsync(NewsItem item)
        {
            if (string.IsNullOrEmpty(item.ID)) throw new ArgumentNullException(nameof(item));
            return Database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(NewsItem item)
        {
            return Database.DeleteAsync(item);
        }

        public async Task<bool> ItemExists(string id)
        {
            return await Database.Table<NewsItem>().Where(i => i.ID == id).FirstOrDefaultAsync() != null;
        }

        public Task<List<NewsItem>> GetItemsAsync()
        {
            return Database.Table<NewsItem>().ToListAsync();
        }
    }
}
