using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infrastructure.Data.ReadWriteDbContext;

namespace Infrastructure.Data
{
    public abstract class DbContextBase : IDisposable
    {
        protected WriteAppDbContext _writeContext;
        protected ReadAppDbContext _readContext;
        protected readonly IDbConnection _dbConnection;
        protected DbContextBase(WriteAppDbContext writeContext, ReadAppDbContext readContext, IDbConnection dbConnection)
        {
            _writeContext = writeContext;
            _readContext = readContext;
            _dbConnection = dbConnection;
        }

        protected abstract void ConfigureEntities(ModelBuilder modelBuilder);

        public void Dispose()
        {
            _writeContext.Dispose();
            _readContext.Dispose();
            _dbConnection.Dispose();
        }
    }
}
