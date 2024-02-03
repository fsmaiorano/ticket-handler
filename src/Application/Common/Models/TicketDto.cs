using Domain.Constants;

namespace Application.Common.Models;

public class TicketDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public Guid HolderId { get; set; }
    public Guid? SectorId { get; set; }
    public Guid? AssigneeId { get; set; }
}
