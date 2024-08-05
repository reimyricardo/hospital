using Microsoft.EntityFrameworkCore;

namespace Clinic.Data.Entities.Common.Primitives;

public class PagedList<TItem>
{

    private PagedList(List<TItem> items,
                      int page,
                      int pageSize,
                      int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public int Page { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

    public bool HasNexPage => Page * PageSize < TotalCount;

    public bool HasPreviousPage => Page > 1;

    public List<TItem> Items { get; }

    public static async Task<PagedList<TItem>> CreatePagedList(IQueryable<TItem> queryable, int page, int pageSize)
    {
        int totalCount = await queryable.CountAsync();

        List<TItem> items = await queryable.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new(items, page, pageSize, totalCount);
    }
}
