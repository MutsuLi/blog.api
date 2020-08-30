using System;
using System.Collections.Generic;
using System.Text;
using Blog.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Model.EntityFrameworkCore
{
    /// <summary>
    /// 学生map类
    /// </summary>
    public class PostsMap : IEntityTypeConfiguration<Posts>
    {
        /// <summary>
        /// 实体属性配置
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Posts> builder)
        {
            // builder.ToTable(MeowvBlogConsts.DbTablePrefix + DbTableName.Posts);
            builder.HasKey(x => x.bId);
            builder.Property(x => x.bsubmitter).HasMaxLength(60);
            builder.Property(x => x.bsubmitterId).HasColumnType("int").IsRequired();
            builder.Property(x => x.btitle).HasMaxLength(256).IsRequired();
            builder.Property(x => x.bcategory).HasMaxLength(256).IsRequired();
            builder.Property(x => x.bcategoryId).HasColumnType("int");
            builder.Property(x => x.bcontent).HasColumnType("longtext").IsRequired();
            builder.Property(x => x.bcommentNum).HasColumnType("int");
            builder.Property(x => x.btraffic).HasColumnType("int");
            builder.Property(x => x.bRemark).HasColumnType("longtext");
            builder.Property(x => x.bUpdateTime).HasColumnType("datetime");
            builder.Property(x => x.bCreateTime).HasColumnType("datetime");
            builder.Property(x => x.IsDeleted).HasColumnType("bool");
        }
    }
}
