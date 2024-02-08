using Domain.Constants;
using System.Text.Json.Serialization;

namespace Application.Common.Models;

public class UserDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("role")]
    public UserRoles Role { get; set;}
}
