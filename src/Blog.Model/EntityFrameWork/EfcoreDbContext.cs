
using Microsoft.EntityFrameworkCore;
using Blog.Api.Models;

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

            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}