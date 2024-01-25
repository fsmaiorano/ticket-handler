//using System.ComponentModel.DataAnnotations.Schema;
//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infrastructure.Mapping;

//public class PokemonDetailMap : IEntityTypeConfiguration<PokemonDetailEntity>
//{
//    public void Configure(EntityTypeBuilder<PokemonDetailEntity> builder)
//    {
//        builder.ToTable("PokemonDetails");
//        builder.HasKey(p => p.Id);
//        builder.HasIndex(p => p.Id);

//        builder.Property(p => p.Id).HasColumnName("id").IsRequired().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
//        builder.Property(p => p.PokemonId).HasColumnName("pokemon_id").IsRequired();
//        builder.Property(p => p.ExternalId).HasColumnName("external_id").IsRequired();
//        builder.Property(p => p.Height).HasColumnName("height").IsRequired();
//        builder.Property(p => p.Weight).HasColumnName("weight").IsRequired();
//        builder.Property(p => p.EvolvesFromPokemonExternalId).HasColumnName("evolves_from_pokemon_external_id").IsRequired();

//        builder.HasOne(p => p.Pokemon).WithOne(p => p.PokemonDetail).HasForeignKey<PokemonDetailEntity>(p => p.PokemonId).HasConstraintName("FK_PokemonDetails_Pokemons_PokemonId").OnDelete(DeleteBehavior.Cascade);
//    }
//}
