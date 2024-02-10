using Domain.Entities;

namespace Application.Common.Models;

public class SectorDto : BaseDto
{
    public string? Name { get; set; }
    public Guid? HolderId { get; set; }
    public HolderEntity? HolderEntity { get; set; }
    public List<UserEntity>? Users { get; set; }

    public SectorDto()
    {
        Users = [];
    }
}
