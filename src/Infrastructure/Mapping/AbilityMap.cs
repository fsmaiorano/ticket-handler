//using System.ComponentModel.DataAnnotations.Schema;
//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infrastructure.Mapping;

//public class AbilityMap : IEntityTypeConfiguration<AbilityEntity>
//{
//    public void Configure(EntityTypeBuilder<AbilityEntity> builder)
//    {
//        builder.ToTable("Abilities");
//        builder.HasKey(p => p.Id);
//        builder.HasIndex(p => p.Id);

//        builder.Property(p => p.Id).HasColumnName("id").IsRequired().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
//        builder.Property(p => p.ExternalId).HasColumnName("external_id").IsRequired();
//        builder.Property(p => p.Name).HasColumnName("name").IsRequired();
//        builder.Property(p => p.Url).HasColumnName("url").IsRequired();
//    }
//}

