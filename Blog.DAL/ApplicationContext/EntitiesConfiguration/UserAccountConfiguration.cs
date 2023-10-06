using Blog.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.ApplicationContext.EntitiesConfiguration
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
        }
    }
}
