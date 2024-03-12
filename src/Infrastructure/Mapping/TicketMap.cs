using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Mapping
{
    public class TicketMap : IEntityTypeConfiguration<TicketEntity>
    {
        public void Configure(EntityTypeBuilder<TicketEntity> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("id").IsRequired().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
            builder.Property(p => p.Title).HasColumnName("title").IsRequired().HasMaxLength(100);
            builder.Property(p => p.Content).HasColumnName("content").IsRequired().HasMaxLength(1000);
            builder.Property(p => p.StatusId).HasColumnName("status").IsRequired().HasMaxLength(100);
            builder.Property(p => p.PriorityId).HasColumnName("priority").IsRequired().HasMaxLength(100);
            builder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(p => p.HolderId).HasColumnName("holder_id").IsRequired();
            builder.Property(p => p.SectorId).HasColumnName("sector_id").IsRequired();
            builder.Property(p => p.AssigneeId).HasColumnName("assignee_id");

            builder.HasOne(p => p.User).WithMany(p => p.Tickets).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Holder).WithMany(p => p.Tickets).HasForeignKey(p => p.HolderId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Sector).WithMany(p => p.Tickets).HasForeignKey(p => p.SectorId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Status).WithMany(p => p.Tickets).HasForeignKey(p => p.StatusId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Priority).WithMany(p => p.Tickets).HasForeignKey(p => p.PriorityId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(p => p.Answers).WithOne(p => p.Ticket).HasForeignKey(p => p.TicketId).OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
