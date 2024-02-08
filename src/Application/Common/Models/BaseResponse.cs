namespace Application.Common.Models;

public class BaseResponse()
{
    public bool Success { get; set; } = false;
    public string? Message { get; set; }
}
