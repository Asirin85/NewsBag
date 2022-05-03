using NewsBag.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsBag.Services
{
    public interface IParser
    {
        Task<IEnumerable<NewsItem>> GetNews();
    }
}
