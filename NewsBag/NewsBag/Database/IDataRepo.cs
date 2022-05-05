using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsBag.Database
{
    public interface IDataRepo<T>
    {
        Task<int> AddItemAsync(T item);
        Task<int> DeleteItemAsync(T item);
        Task<bool> ItemExists(string id);
        Task<List<T>> GetItemsAsync();
    }
}
