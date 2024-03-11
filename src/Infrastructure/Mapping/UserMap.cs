using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("id").IsRequired().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
            builder.Property(p => p.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
            builder.Property(p => p.Email).HasColumnName("email").IsRequired().HasMaxLength(100);
            builder.Property(p => p.Password).HasColumnName("password").IsRequired().HasMaxLength(100);

            builder.HasMany(p => p.Tickets).WithOne(p => p.User).HasForeignKey(p => p.UserId);
        }
    }
}
