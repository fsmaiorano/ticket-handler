using Domain.Common;
using Domain.Constants;

namespace Domain.Entities;

public class UserEntity : BaseEntity
{
    public required string Username { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required UserRoles Role { get; set; }
    public Guid HolderId { get; set; }
    public HolderEntity? Holder { get; set; }
    public List<SectorEntity>? Sectors { get; set; }
}
