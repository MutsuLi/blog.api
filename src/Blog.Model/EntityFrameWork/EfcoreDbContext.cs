
using Microsoft.EntityFrameworkCore;
using Blog.Api.Models;
using Blog.Api.Common.DB;
using Blog.Api.Common;

namespace Blog.Api.Model.EntityFrameworkCore
{
    public class EfcoreDbContext : DbContext
    {
        public EfcoreDbContext(DbContextOptions<EfcoreDbContext> options) : base(options)
        {
        }

        #region DbSet

        public DbSet<BlogArticle> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<sysUserInfo> sysUserInfo { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<RoleModulePermission> RoleModulePermission { get; set; }

        #endregion DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configure();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // 默认添加主数据库连接
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

                case DataBaseType.Sqlite:
                    optionsBuilder.UseMySql(mainConnetctDb.Connection);
                    break;
                default:
                    optionsBuilder.UseMySql(mainConnetctDb.Connection);
                    break;
            }
        }
    }
}