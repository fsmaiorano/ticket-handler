using Domain.Constants;

namespace Application.Common.Models;

public class TicketDto : BaseDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Status { get; set; }
    public string? Priority { get; set; }
    public List<AnswerDto>? Answers { get; set; }
    public Guid UserId { get; set; }
    public Guid HolderId { get; set; }
    public Guid SectorId { get; set; }
    public Guid? AssigneeId { get; set; }
}
