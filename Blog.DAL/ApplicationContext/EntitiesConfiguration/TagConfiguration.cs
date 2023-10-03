using System;
using Blog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DAL.ApplicationContext.EntitiesConfiguration
{
	public class TagConfiguration : IEntityTypeConfiguration<Tag>
	{
		public void Configure(EntityTypeBuilder<Tag> builder)
		{
			builder.ToTable("Tags");

			builder.HasIndex(t => t.Name).IsUnique();
		}
	}
}

