using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public abstract class DbContextBase : IDisposable
    {
        protected AppDbContext _context;
        protected readonly IDbConnection _dbConnection;
        protected DbContextBase(AppDbContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;
        }

        protected abstract void ConfigureEntities(ModelBuilder modelBuilder);

        public void Dispose()
        {
           _context.Dispose();
            _dbConnection.Dispose();
        }
    }
}
