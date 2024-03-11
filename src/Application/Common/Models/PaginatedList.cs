using Microsoft.EntityFrameworkCore;

namespace Application.Common.Models;

public record PaginatedList<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public string? PreviousPage { get; set; }
    public string? NextPage { get; set; }

    public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize, string? previousPage, string? nextPage)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
        PreviousPage = previousPage;
        NextPage = nextPage;
    }
    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        // var urls = (Environment.GetEnvironmentVariable("ASPNETCORE_URLS")!.Split(";") ?? []).FirstOrDefault();

        // var previousPage = pageNumber > 1 ? $"{urls}/pokemon?pageNumber={pageNumber - 1}&pageSize={pageSize}" : null;
        // var nextPage = pageNumber < count ? $"{urls}/pokemon?pageNumber={pageNumber + 1}&pageSize={pageSize}" : null;

        var previousPage = pageNumber > 1 ? $"?pageNumber={pageNumber - 1}&pageSize={pageSize}" : null;
        var nextPage = pageNumber < count ? $"?pageNumber={pageNumber + 1}&pageSize={pageSize}" : null;

        return new PaginatedList<T>(items, count, pageNumber, pageSize, previousPage, nextPage);
    }
}
