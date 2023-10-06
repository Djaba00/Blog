using System;
using Blog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DAL.ApplicationContext.EntitiesConfiguration
{
	public class ArticleConfiguration : IEntityTypeConfiguration<Article>
	{
		public void Configure(EntityTypeBuilder<Article> builder)
		{
            builder.ToTable("Articles");

            builder.Property(a => a.Title).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Content).IsRequired();
        }
	}
}

