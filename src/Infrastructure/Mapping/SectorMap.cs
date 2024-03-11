using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Mapping;

public class SectorMap : IEntityTypeConfiguration<SectorEntity>
{
    public void Configure(EntityTypeBuilder<SectorEntity> builder)
    {
        builder.ToTable("Sectors");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id").IsRequired().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
        builder.Property(p => p.Name).HasColumnName("name").IsRequired().HasMaxLength(100);

        builder.HasOne(p => p.HolderEntity)
            .WithMany(p => p.Sectors)
            .HasForeignKey(p => p.HolderId)
            .HasConstraintName("FK_Sectors_Holders_HolderId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Users)
        .WithMany(p => p.Sectors)
        .UsingEntity<Dictionary<string, object>>("UserSectors",
            sector => sector.HasOne<UserEntity>()
                            .WithMany()
                            .HasForeignKey("UserId")
                            .HasConstraintName("FK_UserSectors_Users_UserId")
                            .OnDelete(DeleteBehavior.NoAction),
            user => user.HasOne<SectorEntity>()
                        .WithMany()
                        .HasForeignKey("SectorId")
                        .HasConstraintName("FK_UserSectors_Sectors_SectorId")
                        .OnDelete(DeleteBehavior.NoAction),
            x =>
            {
                x.HasKey("UserId", "SectorId");
                x.HasIndex("SectorId");
                x.HasIndex("UserId");
                x.ToTable("UserSectors");
            });
    }
}
