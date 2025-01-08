using Core.Entities;
using Core.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Infrastructure.Data.ReadWriteDbContext;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : DbContextBase, IUnitOfWork
    {
        public UnitOfWork(WriteAppDbContext writeContext, ReadAppDbContext readContext, IDbConnection dbConnection) : base(writeContext, readContext, dbConnection)
        {
            Users = new Repository<User, int>(_writeContext,_readContext, _dbConnection);
        }

        public IRepository<User, int> Users { get; }

        public async Task<int> CompleteAsync()
        {
            return await _writeContext.SaveChangesAsync();
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using (var transaction = await _writeContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await action();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"RollBack {ex.Message}");
                    throw;
                }
            }
        }

        protected override void ConfigureEntities(ModelBuilder modelBuilder)
        {
        }
    }
}
