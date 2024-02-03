using Domain.Entities;

namespace Application.Common.Models;

public class SectorDto
{
    public string? Name { get; set; }
    public Guid? HolderId { get; set; }
    public HolderEntity? HolderEntity { get; set; }
    public List<UserEntity>? Users { get; set; }
}
