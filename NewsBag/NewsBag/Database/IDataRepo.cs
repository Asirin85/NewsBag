using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsBag.Database
{
    public interface IDataRepo<T>
    {
        Task<int> AddItemAsync(T item);
        Task<int> DeleteItemAsync(T item);
        //Task<T> GetItemAsync(string id);
        Task<bool> ItemExists(string id);
        Task<List<T>> GetItemsAsync();
    }
}
