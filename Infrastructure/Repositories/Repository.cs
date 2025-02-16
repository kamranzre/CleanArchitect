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
            //var entity = await _entity.FindAsync(id);
            //if (entity != null)
            //{
            //    _entity.Remove(entity);
            //    await _context.SaveChangesAsync();

            //    // حذف موجودیت از کش
            //    var cacheKey = $"{typeof(TEntity).Name}-{id}";
            //    await _cacheService.RemoveAsync(cacheKey);

            //    // به‌روزرسانی لیست کش‌شده
            //    var allEntities = await GetAllAsync();
            //    await _cacheService.SetAsync($"{typeof(TEntity).Name}-all", allEntities, TimeSpan.FromMinutes(30));
            //}
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

        public async Task UpdateAsync(TEntity entity)
        {
            _entity.Update(entity);
            await _context.SaveChangesAsync();

            //// اگر این موجودیت در کش وجود داشت، مقدار آن را به‌روز کنیم
            //var cacheKey = $"{typeof(TEntity).Name}-{entity.Id}";
            //await _cacheService.SetAsync(cacheKey, entity, TimeSpan.FromMinutes(30));

            //// چون کل لیست کش شده است، باید آن را نیز به‌روز کنیم
            //var allEntities = await GetAllAsync();
            //await _cacheService.SetAsync($"{typeof(TEntity).Name}-all", allEntities, TimeSpan.FromMinutes(30));
        }

        protected override void ConfigureEntities(ModelBuilder modelBuilder)
        {
        }
    }
}
