using System.Text.Json.Serialization;

namespace Domain.Common;

public abstract class BaseEntity
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }
}
