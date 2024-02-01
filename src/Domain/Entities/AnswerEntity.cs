using Domain.Common;

namespace Domain.Entities;

public class AnswerEntity : BaseEntity
{
    public required string Content { get; set; }
    public required Guid TicketId { get; set; }
    public TicketEntity? Ticket { get; set; }
    public required Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public required Guid HolderId { get; set; }
    public required Guid? SectorId { get; set; }
}
