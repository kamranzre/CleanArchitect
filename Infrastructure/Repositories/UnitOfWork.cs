﻿using Core.Entities;
using Core.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : DbContextBase, IUnitOfWork
    {
        public UnitOfWork(AppDbContext context, IDbConnection dbConnection) : base(context, dbConnection)
        {
            Users = new Repository<User>(_context, _dbConnection);
        }

        public IRepository<User> Users { get; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
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
