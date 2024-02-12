namespace Application.Common.Models;

public class SectorDto : BaseDto
{
    public string? Name { get; set; }
    public Guid HolderId { get; set; }

    public SectorDto()
    {

    }
}
