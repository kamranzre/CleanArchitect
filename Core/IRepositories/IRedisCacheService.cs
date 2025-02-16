using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IRedisCacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan expiration);

        Task<T> GetAsync<T>(string key);

        Task RemoveAsync(string key);
    }
}
