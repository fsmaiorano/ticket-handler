using Domain.Common;

namespace Domain.Entities;

public class HolderEntity : BaseEntity
{
    public required string Name { get; set; }
    public List<SectorEntity>? Sectors { get; set; }
    public virtual List<TicketEntity>? Tickets { get; set; }

    public HolderEntity()
    {
        Sectors = [];
    }
}
