namespace Application;

public class PaginatedBaseResponse
{
    public bool Success { get; set; } = false;
    public string? Message { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    //public int TotalCount { get; set; }
    public string? PreviousPage { get; set; }
    public string? NextPage { get; set; }
}
