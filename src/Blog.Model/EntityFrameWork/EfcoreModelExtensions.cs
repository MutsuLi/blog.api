using Blog.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Model.EntityFrameworkCore
{
    public static class EfcoreModelExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {

            builder.Entity<BlogArticle>(b =>
            {
                // b.ToTable(MeowvBlogConsts.DbTablePrefix + DbTableName.Posts);
                b.HasKey(x => x.bId);
                b.Property(x => x.bsubmitter).HasMaxLength(60);
                b.Property(x => x.bsubmitterId).HasColumnType("int").IsRequired();
                b.Property(x => x.btitle).HasMaxLength(256).IsRequired();
                b.Property(x => x.bcategory).HasMaxLength(256).IsRequired();
                b.Property(x => x.bcategoryId).HasColumnType("int");
                b.Property(x => x.bcontent).HasColumnType("longtext").IsRequired();
                b.Property(x => x.bcommentNum).HasColumnType("int");
                b.Property(x => x.btraffic).HasColumnType("int");
                b.Property(x => x.bRemark).HasColumnType("longtext").IsRequired();
                b.Property(x => x.bUpdateTime).HasColumnType("datetime");
                b.Property(x => x.bCreateTime).HasColumnType("datetime");
                b.Property(x => x.IsDeleted).HasColumnType("bool");
            });

            builder.Entity<sysUserInfo>(b =>
            {
                //b.ToTable(MeowvBlogConsts.DbTablePrefix + DbTableName.Categories);
                b.HasKey(x => x.uId);
                b.Property(x => x.uEmail).HasMaxLength(200).IsRequired();
                b.Property(x => x.uPassword).HasMaxLength(200).IsRequired();
                b.Property(x => x.uName).HasMaxLength(200).IsRequired();
                b.Property(x => x.uAvatar).HasMaxLength(200);
                b.Property(x => x.uTitle).HasMaxLength(200);
                b.Property(x => x.uDescription).HasColumnType("longtext");
                b.Property(x => x.uRemark).HasColumnType("longtext");
                b.Property(x => x.uUpdateTime).HasColumnType("datetime");
                b.Property(x => x.uCreateTime).HasColumnType("datetime");
                b.Property(x => x.uLastErrTime).HasColumnType("datetime");
                b.Property(x => x.uErrorCount).HasColumnType("int");
                b.Property(x => x.gender).HasColumnType("int");
                b.Property(x => x.age).HasColumnType("int");
                b.Property(x => x.birth).HasColumnType("datetime");
                b.Property(x => x.addr).HasMaxLength(200);
                b.Property(x => x.tdIsDelete).HasColumnType("bool");
            });

            builder.Entity<Tag>(b =>
            {
                //b.ToTable(MeowvBlogConsts.DbTablePrefix + DbTableName.Tags);
                b.HasKey(x => x.tId);
                b.Property(x => x.tName).HasMaxLength(256).IsRequired();
                b.Property(x => x.tDispalyName).HasMaxLength(256).IsRequired();
                b.Property(x => x.tDescription).HasColumnType("longtext");
                b.Property(x => x.tModifyTime).HasColumnType("datetime");
                b.Property(x => x.tCreateTime).HasColumnType("datetime");
                b.Property(x => x.tIcon).HasMaxLength(200);
                b.Property(x => x.tsubmitter).HasMaxLength(60);
                b.Property(x => x.tsubmitterId).HasColumnType("int").IsRequired();
                b.Property(x => x.IsDeleted).HasColumnType("bool");
            });

            builder.Entity<UserRole>(b =>
            {
                //b.ToTable(MeowvBlogConsts.DbTablePrefix + DbTableName.PostTags);
                b.HasKey(x => x.Id);
                b.Property(x => x.UserId).HasColumnType("int").IsRequired();
                b.Property(x => x.RoleId).HasColumnType("int").IsRequired();
                b.Property(x => x.CreateId).HasColumnType("int");
                b.Property(x => x.CreateBy).HasMaxLength(50);
                b.Property(x => x.CreateTime).HasColumnType("datetime");
                b.Property(x => x.ModifyId).HasColumnType("int");
                b.Property(x => x.ModifyBy).HasMaxLength(50);
                b.Property(x => x.ModifyTime).HasColumnType("datetime");
                b.Property(x => x.IsDeleted).HasColumnType("bool");
            });

            builder.Entity<Role>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).HasMaxLength(50).IsRequired();
                b.Property(x => x.Description).HasMaxLength(100).IsRequired();
                b.Property(x => x.OrderSort).HasColumnType("int");
                b.Property(x => x.Enabled).HasColumnType("bool");
                b.Property(x => x.CreateId).HasColumnType("int");
                b.Property(x => x.CreateBy).HasMaxLength(50);
                b.Property(x => x.CreateTime).HasColumnType("datetime");
                b.Property(x => x.ModifyId).HasColumnType("int");
                b.Property(x => x.ModifyBy).HasMaxLength(50);
                b.Property(x => x.ModifyTime).HasColumnType("datetime");
                b.Property(x => x.IsDeleted).HasColumnType("bool");
            });

            builder.Entity<Module>(b =>
            {
                //b.ToTable(MeowvBlogConsts.DbTablePrefix + DbTableName.Wallpapers);
                b.HasKey(x => x.Id);
                b.Property(x => x.ParentId).HasColumnType("int");
                b.Property(x => x.Name).HasMaxLength(50).IsRequired();
                b.Property(x => x.LinkUrl).HasMaxLength(100).IsRequired();
                b.Property(x => x.Area).HasColumnType("longtext");
                b.Property(x => x.Controller).HasColumnType("longtext");
                b.Property(x => x.Action).HasColumnType("longtext");
                b.Property(x => x.Icon).HasMaxLength(100);
                b.Property(x => x.Code).HasMaxLength(10);
                b.Property(x => x.OrderSort).HasColumnType("int");
                b.Property(x => x.Description).HasMaxLength(100);
                b.Property(x => x.Enabled).HasColumnType("bool").IsRequired();
                b.Property(x => x.IsMenu).HasColumnType("bool").IsRequired();
                b.Property(x => x.CreateId).HasColumnType("int");
                b.Property(x => x.CreateBy).HasMaxLength(100);
                b.Property(x => x.CreateTime).HasColumnType("datetime");
                b.Property(x => x.ModifyId).HasColumnType("int");
                b.Property(x => x.ModifyBy).HasMaxLength(100);
                b.Property(x => x.ModifyTime).HasColumnType("datetime");
            });

            builder.Entity<ModulePermission>(b =>
            {
                //b.ToTable(MeowvBlogConsts.DbTablePrefix + DbTableName.HotNews);
                b.HasKey(x => x.Id);
                b.Property(x => x.ModuleId).HasColumnType("int");
                b.Property(x => x.PermissionId).HasColumnType("int");
                b.Property(x => x.CreateId).HasColumnType("int");
                b.Property(x => x.CreateBy).HasMaxLength(50);
                b.Property(x => x.CreateTime).HasColumnType("datetime");
                b.Property(x => x.ModifyId).HasColumnType("int");
                b.Property(x => x.ModifyBy).HasMaxLength(50);
                b.Property(x => x.ModifyTime).HasColumnType("datetime");
                b.Property(x => x.IsDeleted).HasColumnType("bool");
            });

            builder.Entity<RoleModulePermission>(b =>
            {
                //b.ToTable(MeowvBlogConsts.DbTablePrefix + DbTableName.Signatures);
                b.HasKey(x => x.Id);
                b.Property(x => x.ModuleId).HasColumnType("int").IsRequired();
                b.Property(x => x.RoleId).HasColumnType("int").IsRequired();
                b.Property(x => x.PermissionId).HasColumnType("int");
                b.Property(x => x.CreateId).HasColumnType("int");
                b.Property(x => x.CreateBy).HasMaxLength(50);
                b.Property(x => x.CreateTime).HasColumnType("datetime");
                b.Property(x => x.ModifyId).HasColumnType("int");
                b.Property(x => x.ModifyBy).HasMaxLength(50);
                b.Property(x => x.ModifyTime).HasColumnType("datetime");
                b.Property(x => x.IsDeleted).HasColumnType("bool");
            });
        }
    }
}