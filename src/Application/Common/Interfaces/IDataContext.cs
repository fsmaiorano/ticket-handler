using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IDataContext
{
    //DbSet<PokemonEntity> Pokemons { get; }
    //DbSet<AbilityEntity> Abilities { get; }
    //DbSet<MoveEntity> Moves { get; }
    //DbSet<TypeEntity> Types { get; }
    //DbSet<SpriteEntity> Sprites { get; }
    //DbSet<PokemonDetailEntity> Details { get; }
    DbSet<UserEntity> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
