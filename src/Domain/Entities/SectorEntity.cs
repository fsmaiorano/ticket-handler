using Domain.Common;

namespace Domain.Entities;

public class SectorEntity : BaseEntity
{
    public required string Name { get; set; }
    public required Guid HolderId { get; set; }
    public HolderEntity? HolderEntity { get; set; }
    public List<UserEntity>? Users { get; set; }

    public SectorEntity()
    {
        Users = [];
    }
}
