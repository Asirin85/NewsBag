using NewsBag.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsBag.Database
{
    public class DatabaseConnection
    {
        private static SQLiteAsyncConnection _connection;
        public async static Task<SQLiteAsyncConnection> GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SQLiteAsyncConnection(GlobalNewsConstants.DatabasePath, GlobalNewsConstants.Flags);
                await _connection.CreateTableAsync<NewsItem>();
            }
            return _connection;
        }
    }
}
