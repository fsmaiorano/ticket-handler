using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Mapping;

public class HolderMap : IEntityTypeConfiguration<HolderEntity>
{
    public void Configure(EntityTypeBuilder<HolderEntity> builder)
    {
        builder.ToTable("Holders");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id").IsRequired().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
        builder.Property(p => p.Name).HasColumnName("name").IsRequired().HasMaxLength(100);

        builder.HasMany(p => p.Sectors).WithOne(p => p.HolderEntity).HasForeignKey(p => p.HolderId).HasConstraintName("FK_Sectors_Holders_HolderId").OnDelete(DeleteBehavior.Cascade);
    }
}
