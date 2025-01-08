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
using static Infrastructure.Data.ReadWriteDbContext;

namespace Infrastructure.Repositories
{
    public class Repository<TEntity, TKey> : DbContextBase, IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbSet<TEntity> _writeEntity;
        private readonly DbSet<TEntity> _readEntity;
        public Repository(WriteAppDbContext writeContext, ReadAppDbContext readContext, IDbConnection dbConnection) : base(writeContext, readContext, dbConnection)
        {
            _writeEntity = writeContext.Set<TEntity>();
            _readEntity = readContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _writeEntity.AddAsync(entity);
        }

        public async Task<TKey> AddResponseAsync(TEntity entity)
        {
            if (entity != null)
            {
                var add = await _writeEntity.AddAsync(entity);
                await _writeContext.SaveChangesAsync();
                var idProperty = add.Property("Id");
                if (idProperty != null)
                {
                    return (TKey)Convert.ChangeType(idProperty.CurrentValue, typeof(TKey));
                }
            }
            return default;
        }

        public Task DeleteAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _readEntity.ToListAsync();
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
