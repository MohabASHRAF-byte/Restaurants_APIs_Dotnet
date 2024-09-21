namespace Restuarants.Application.Common;

public class PageResult<T>(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber)
{
    public IEnumerable<T> Items { get; set; } = items;
    public int TotalItemsCount { get; set; } = totalCount;
    public int TotalPages { get; set; } = ((totalCount + pageSize - 1) / pageSize);
    public int ItemsFrom { get; set; } = pageSize * (pageNumber - 1) + 1;
    public int ItemsTo { get; set; } = (pageSize * (pageNumber - 1) + 1) + pageSize - 1;
}
