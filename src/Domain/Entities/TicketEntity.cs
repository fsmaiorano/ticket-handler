using Domain.Common;
using Domain.Constants;

namespace Domain.Entities;

public class TicketEntity : BaseEntity
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required TicketStatus Status { get; set; }
    public required TicketPriority Priority { get; set; }
    public required Guid UserId { get; set; }
    public required Guid HolderId { get; set; }
    public required Guid SectorId { get; set; }
    public required Guid AssigneeId { get; set; }

    public virtual UserEntity? User { get; set; }
    public virtual HolderEntity? Holder { get; set; }
    public virtual SectorEntity? Sector { get; set; }
    public virtual UserEntity? Assignee { get; set; }
}
