namespace Application.Common.Models;
public class PaginatedDto<T>
{
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    //public int TotalCount { get; set; }
    public string? PreviousPage { get; set; }
    public string? NextPage { get; set; }
    public List<T>? Items { get; set; }
}