namespace Restaurats.Application.Common;
public class PaginatedResult<T>(IEnumerable<T> items, int count, int pageNumber, int pageSize)
{
    public IEnumerable<T> Items { get; } = items;
    public int TotalCount { get; } = count;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    
    public static PaginatedResult<T> Create(IEnumerable<T> source, int pageNumber=1, int pageSize=10)
    {
        int count = source.Count();
        if(pageNumber< 1)
            pageNumber = 1;
        if(pageSize<10)
            pageSize = 10;
        var result = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedResult<T>(result, count, pageNumber, pageSize);
    }
}
