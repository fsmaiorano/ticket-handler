namespace Domain.Common;

public abstract class BaseEntity
{
    // [JsonIgnore]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsActive { get; set; }

    public BaseEntity()
    {
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
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
