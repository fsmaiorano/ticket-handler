//using System.ComponentModel.DataAnnotations.Schema;
//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infrastructure.Mapping;

//public class SpriteMap : IEntityTypeConfiguration<SpriteEntity>
//{
//    public void Configure(EntityTypeBuilder<SpriteEntity> builder)
//    {
//        builder.ToTable("Sprites");
//        builder.HasKey(p => p.Id);
//        builder.HasIndex(p => p.Id);

//        builder.Property(p => p.Id).HasColumnName("id").IsRequired().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
//        builder.Property(p => p.PokemonId).HasColumnName("pokemon_id").IsRequired();
//        builder.Property(p => p.ExternalId).HasColumnName("external_id").IsRequired();
//        builder.Property(p => p.BackDefault).HasColumnName("back_default").IsRequired(false);
//        builder.Property(p => p.BackFemale).HasColumnName("back_female").IsRequired(false);
//        builder.Property(p => p.BackShiny).HasColumnName("back_shiny").IsRequired(false);
//        builder.Property(p => p.BackShinyFemale).HasColumnName("back_shiny_female").IsRequired(false);
//        builder.Property(p => p.FrontDefault).HasColumnName("front_default").IsRequired(false);
//        builder.Property(p => p.FrontFemale).HasColumnName("front_female").IsRequired(false);
//        builder.Property(p => p.FrontShiny).HasColumnName("front_shiny").IsRequired(false);
//        builder.Property(p => p.FrontShinyFemale).HasColumnName("front_shiny_female").IsRequired(false);
//        builder.Property(p => p.DreamWorldFrontDefault).HasColumnName("dream_world_front_default").IsRequired(false);
//        builder.Property(p => p.DreamWorldFrontFemale).HasColumnName("dream_world_front_female").IsRequired(false);
//        builder.Property(p => p.HomeFrontDefault).HasColumnName("home_front_default").IsRequired(false);
//        builder.Property(p => p.HomeFrontFemale).HasColumnName("home_front_female").IsRequired(false);
//        builder.Property(p => p.HomeFrontShiny).HasColumnName("home_front_shiny").IsRequired(false);
//        builder.Property(p => p.HomeFrontShinyFemale).HasColumnName("home_front_shiny_female").IsRequired(false);
//        builder.Property(p => p.OfficialArtworkFrontDefault).HasColumnName("official_artwork_front_default").IsRequired(false);
//        builder.Property(p => p.OfficialArtworkFrontShiny).HasColumnName("official_artwork_front_shiny").IsRequired(false);

//        builder.HasOne(p => p.Pokemon).WithOne(p => p.Sprites).HasForeignKey<SpriteEntity>(p => p.PokemonId).HasConstraintName("FK_Sprites_Pokemons_PokemonId").OnDelete(DeleteBehavior.Cascade);
//    }
//}
