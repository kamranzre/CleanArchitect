using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T product);
        Task UpdateAsync(T product);
        Task DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllDapperAsync();
    }
}
