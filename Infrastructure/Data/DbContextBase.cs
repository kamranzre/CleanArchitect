using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        protected readonly IConfiguration _configuration;
        protected DbContextBase(WriteAppDbContext writeContext, ReadAppDbContext readContext, IDbConnection dbConnection, IConfiguration configuration)
        {
            _writeContext = writeContext;
            _readContext = readContext;
            _dbConnection = dbConnection;
            _configuration = configuration;
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
