using Core.Entities;
using Core.IRepositories;
using Dapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;
        public Repository(WriteAppDbContext writeContext, ReadAppDbContext readContext, IDbConnection dbConnection, IConfiguration configuration) 
            : base(writeContext, readContext, dbConnection, configuration)
        {
            _writeEntity = writeContext.Set<TEntity>();
            _readEntity = readContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _writeEntity.AddAsync(entity);
        }

        public async Task<TKey?> AddResponseAsync(TEntity entity)
        {
            if (entity != null)
            {
                var add = await _writeEntity.AddAsync(entity);
                await _writeContext.SaveChangesAsync();
                var ValurOfProperty = add.Property("Id")?.CurrentValue;
                if (ValurOfProperty != null)
                {
                    return (TKey)Convert.ChangeType(ValurOfProperty, typeof(TKey));
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
                connection.ConnectionString = _configuration.GetConnectionString("ReadShop");
                var query = $"SELECT * FROM {typeof(TEntity).Name}s";
                return await connection.QueryAsync<TEntity>(query);
            }
        }

        public async Task InsertListDapperAsync(string query, List<TEntity> T)
        {
            using (var connection = _dbConnection)
            {
                connection.Open();
                await connection.ExecuteAsync(query, T);
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
