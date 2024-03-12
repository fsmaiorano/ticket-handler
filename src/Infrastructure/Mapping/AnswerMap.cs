using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Mapping;

public class AnswerMap : IEntityTypeConfiguration<AnswerEntity>
{
    public void Configure(EntityTypeBuilder<AnswerEntity> builder)
    {
        builder.ToTable("Answers");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id").IsRequired().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
        builder.Property(p => p.Content).HasColumnName("content").IsRequired().HasMaxLength(1000);
        builder.Property(p => p.TicketId).HasColumnName("ticket_id").IsRequired();
        builder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(p => p.HolderId).HasColumnName("holder_id").IsRequired();
        builder.Property(p => p.SectorId).HasColumnName("sector_id").IsRequired();
    }
}
