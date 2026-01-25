using Localizator.Shared.Providers;

namespace Localizator.Shared.Result;

public class Pagination
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public long TotalItems { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }

    public static Pagination Empty() => new()
    {
        Page = 1,
        PageSize = 0,
        TotalItems = 0,
        TotalPages = 1,
        HasNextPage = false,
        HasPreviousPage = false
    };

    public void ApplyToMeta(Meta meta) => meta.Pagination = this;

    public void ApplyToMeta() => MetaProvider.Get().Pagination = this;

    public static int SanitizePageSize(int requested, int max) => Math.Min(requested, max);

    public static Pagination FromList<T>(IList<T> source, int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var totalItems = source.Count;
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        var hasNext = page < totalPages;
        var hasPrev = page > 1;

        return new Pagination
        {
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages,
            HasNextPage = hasNext,
            HasPreviousPage = hasPrev
        };
    }

    public static IList<T> PaginateList<T>(IList<T> source, int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        return source.Count > skip ? source.Skip(skip).Take(pageSize).ToList() : new List<T>();
    }
}
