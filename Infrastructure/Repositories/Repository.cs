using Core.IRepositories;
using Dapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : DbContextBase,IRepository<T> where T : class
    {
        private readonly DbSet<T> _entity;
        public Repository(AppDbContext context, IDbConnection dbConnection) : base(context, dbConnection)
        {
            _entity = context.Set<T>();
        }

        public async Task AddAsync(T product)
        {
            await _entity.AddAsync(product);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllDapperAsync()
        {
            using (var connection = _dbConnection)
            {
                var query = $"SELECT * FROM {typeof(T).Name}s";
                return await connection.QueryAsync<T>(query);
            }
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T product)
        {
            throw new NotImplementedException();
        }

        protected override void ConfigureEntities(ModelBuilder modelBuilder)
        {
        }
    }
}
