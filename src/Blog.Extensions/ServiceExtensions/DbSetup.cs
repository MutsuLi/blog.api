using Blog.Api.Models;
using Blog.Api.Model.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Blog.Api.Common.DB;
using Blog.Api.Common;
using Microsoft.EntityFrameworkCore;
namespace Blog.Api.Extensions
{
    /// <summary>
    /// Db 启动服务
    /// </summary>
    public static class DbSetup
    {
        public static void AddDbSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddScoped<DBSeed>();
            services.AddScoped<sugarDbContext>();
            services.AddScoped<EfcoreDbContext>();
            services.AddDbContext<EfcoreDbContext>(optionsBuilder => {
                MainDb.CurrentDbConnId = Appsettings.app(new string[] { "MainDB" });
                var mainConnetctDb = BaseDBConfig.MutiConnectionString.Item1.Find(x => x.ConnId == MainDb.CurrentDbConnId);
                if (BaseDBConfig.MutiConnectionString.Item1.Count > 0)
                {
                    if (mainConnetctDb == null)
                    {
                        mainConnetctDb = BaseDBConfig.MutiConnectionString.Item1[0];
                    }
                }
                switch (mainConnetctDb.DbType)
                {
                    case DataBaseType.MySql:
                        optionsBuilder.UseMySql(mainConnetctDb.Connection);
                        break;
                    default:
                        optionsBuilder.UseMySql(mainConnetctDb.Connection);
                        break;
                }
            });
        }
    }
}
