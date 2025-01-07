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
    public class Repository<TEntity, TKey> : DbContextBase, IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbSet<TEntity> _entity;
        public Repository(AppDbContext context, IDbConnection dbConnection) : base(context, dbConnection)
        {
            _entity = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
        }

        public Task DeleteAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllDapperAsync()
        {
            using (var connection = _dbConnection)
            {
                var query = $"SELECT * FROM {typeof(TEntity).Name}s";
                return await connection.QueryAsync<TEntity>(query);
            }
        }

        public Task<TEntity> GetByIdAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        protected override void ConfigureEntities(ModelBuilder modelBuilder)
        {
        }
    }
}
