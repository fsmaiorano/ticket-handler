using Domain.Common;

namespace Domain.Entities;

public class UserEntity : BaseEntity
{
    public required string Username { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool? IsActive { get; set; }

    public UserEntity()
    {
        IsActive = true;
    }

    public void Enable()
    {
        IsActive = true;
    }

    public void Disable()
    {
        IsActive = false;
    }
}
