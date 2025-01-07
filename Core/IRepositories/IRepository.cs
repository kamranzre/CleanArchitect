using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity product);
        Task UpdateAsync(TEntity product);
        Task DeleteAsync(TKey id);

        Task<IEnumerable<TEntity>> GetAllDapperAsync();
    }
}
