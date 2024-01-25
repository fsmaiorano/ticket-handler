//using System.ComponentModel.DataAnnotations.Schema;
//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infrastructure.Mapping;
//public class PokemonMap : IEntityTypeConfiguration<PokemonEntity>
//{
//    public void Configure(EntityTypeBuilder<PokemonEntity> builder)
//    {
//        builder.ToTable("Pokemons");
//        builder.HasKey(p => p.Id);
//        builder.HasIndex(p => p.Id);

//        builder.Property(p => p.Id).HasColumnName("id").IsRequired().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
//        builder.Property(p => p.ExternalId).HasColumnName("external_id").IsRequired();
//        builder.Property(p => p.Name).HasColumnName("name").IsRequired();
//        builder.Property(p => p.Url).HasColumnName("url").IsRequired();

//        builder.HasMany(p => p.Types).WithMany(p => p.Pokemons).UsingEntity<Dictionary<string, object>>("PokemonTypes",
//            pokemon => pokemon.HasOne<TypeEntity>().WithMany().HasForeignKey("TypeId").HasConstraintName("FK_PokemonTypes_Types_TypeId").OnDelete(DeleteBehavior.Cascade),
//            type => type.HasOne<PokemonEntity>().WithMany().HasForeignKey("PokemonId").HasConstraintName("FK_PokemonTypes_Pokemons_PokemonId").OnDelete(DeleteBehavior.Cascade),
//            x =>
//            {
//                x.HasKey("PokemonId", "TypeId");
//                x.HasIndex("TypeId");
//                x.HasIndex("PokemonId");
//                x.ToTable("PokemonTypes");
//            });

//        builder.HasMany(p => p.Abilities).WithMany(p => p.Pokemons).UsingEntity<Dictionary<string, object>>("PokemonAbilities",
//            pokemon => pokemon.HasOne<AbilityEntity>().WithMany().HasForeignKey("AbilityId").HasConstraintName("FK_PokemonAbilities_Abilities_AbilityId").OnDelete(DeleteBehavior.Cascade),
//            ability => ability.HasOne<PokemonEntity>().WithMany().HasForeignKey("PokemonId").HasConstraintName("FK_PokemonAbilities_Pokemons_PokemonId").OnDelete(DeleteBehavior.Cascade),
//            x =>
//            {
//                x.HasKey("PokemonId", "AbilityId");
//                x.HasIndex("AbilityId");
//                x.HasIndex("PokemonId");
//                x.ToTable("PokemonAbilities");
//            });

//        builder.HasMany(p => p.Moves).WithMany(p => p.Pokemons).UsingEntity<Dictionary<string, object>>("PokemonMoves",
//            pokemon => pokemon.HasOne<MoveEntity>().WithMany().HasForeignKey("MoveId").HasConstraintName("FK_PokemonMoves_Moves_MoveId").OnDelete(DeleteBehavior.Cascade),
//            move => move.HasOne<PokemonEntity>().WithMany().HasForeignKey("PokemonId").HasConstraintName("FK_PokemonMoves_Pokemons_PokemonId").OnDelete(DeleteBehavior.Cascade),
//            x =>
//            {
//                x.HasKey("PokemonId", "MoveId");
//                x.HasIndex("MoveId");
//                x.HasIndex("PokemonId");
//                x.ToTable("PokemonMoves");
//            });

//        builder.HasOne(p => p.Sprites).WithOne(p => p.Pokemon).HasForeignKey<SpriteEntity>(p => p.PokemonId).HasConstraintName("FK_Sprites_Pokemons_PokemonId").OnDelete(DeleteBehavior.Cascade);
//        builder.HasOne(p => p.PokemonDetail).WithOne(p => p.Pokemon).HasForeignKey<PokemonDetailEntity>(p => p.PokemonId).HasConstraintName("FK_PokemonDetails_Pokemons_PokemonId").OnDelete(DeleteBehavior.Cascade);
//    }
//}
