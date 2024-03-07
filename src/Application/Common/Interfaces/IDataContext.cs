using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IDataContext
{
    DbSet<UserEntity> Users { get; }
    DbSet<HolderEntity> Holders { get; }
    DbSet<SectorEntity> Sectors { get; }
    DbSet<TicketEntity> Tickets { get; }
    DbSet<AnswerEntity> Answers { get; }
    DbSet<StatusEntity> Statuses { get; }
    DbSet<PriorityEntity> Priorities { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
