using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Application.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using Core.IRepositories;
using Infrastructure.Repositories;
using Application.Services;
using Core.Entities;

namespace IOC
{
    public static class DependencyContainer
    {
        public static void RegisterService(IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Shop")));

            services.AddScoped<IDbConnection>(db =>
            new SqlConnection(configuration.GetConnectionString("Shop")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRepository<User>, Repository<User>>();

            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
