using Domain.Common;

namespace Domain.Entities;

public class TicketEntity : BaseEntity
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required Guid StatusId { get; set; }
    public required Guid PriorityId { get; set; }
    public required Guid UserId { get; set; }
    public required Guid HolderId { get; set; }
    public required Guid SectorId { get; set; }
    public Guid? AssigneeId { get; set; }
    public List<AnswerEntity>? Answers { get; set; }
    public virtual UserEntity? User { get; set; }
    public virtual HolderEntity? Holder { get; set; }
    public virtual SectorEntity? Sector { get; set; }
    public virtual UserEntity? Assignee { get; set; }
    public virtual StatusEntity? Status { get; set; }
    public virtual PriorityEntity? Priority { get; set; }

    public TicketEntity()
    {
        Answers = [];
    }
}
