namespace Application.Common.Models;

public class HolderDto : BaseDto
{
    public required string Name { get; set; }
    public List<SectorDto>? Sectors { get; set; }

    public HolderDto()
    {
        Sectors = [];
    }
}
