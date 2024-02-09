namespace Application.Common.Models;

public class AnswerDto : BaseDto
{
    public required string Content { get; set; }
    public required Guid TicketId { get; set; }
    public required Guid UserId { get; set; }
    public required Guid HolderId { get; set; }
    public required Guid SectorId { get; set; }
}
