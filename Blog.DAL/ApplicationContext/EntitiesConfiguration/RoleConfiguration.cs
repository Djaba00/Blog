using Blog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DAL.ApplicationContext.EntitiesConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(c => c.Accounts)
                .WithOne(a => a.Role)
                .HasForeignKey(a => a.RoleId);
        }
    }
}
