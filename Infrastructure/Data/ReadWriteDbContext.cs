using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ReadWriteDbContext
    {
        public class WriteAppDbContext : DbContext
        {
            public WriteAppDbContext(DbContextOptions<WriteAppDbContext> options) : base(options)
            {

            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }

            public DbSet<User> Users { get; set; }
        }

        public class ReadAppDbContext : DbContext
        {
            public ReadAppDbContext(DbContextOptions<ReadAppDbContext> options) : base(options)
            {

            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }

            public DbSet<User> Users { get; set; }
        }
    }
}
