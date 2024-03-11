using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping;

public class StatusMap : IEntityTypeConfiguration<StatusEntity>
{
    public void Configure(EntityTypeBuilder<StatusEntity> builder)
    {
        builder.ToTable("Statuses");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id").IsRequired().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
        builder.Property(p => p.Code).HasColumnName("code").IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).HasColumnName("description").IsRequired().HasMaxLength(100);
    }
}
