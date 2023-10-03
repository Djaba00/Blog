using System;
using Blog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DAL.ApplicationContext.EntitiesConfiguration
{
	public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
	{
		public void Configure(EntityTypeBuilder<UserProfile> builder)
		{
			builder.ToTable("UserProfiles");
			builder.Property(u => u.FirstName).IsRequired().HasMaxLength(30);
			builder.Property(u => u.LastName).IsRequired().HasMaxLength(30);
        }
	}
}

