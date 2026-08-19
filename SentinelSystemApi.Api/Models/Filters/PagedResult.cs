namespace SentinelSystemApi.Api.Models.Filters;

public class PagedResult<T>
{
      public IEnumerable<T> Data { get; set; } = [];
      public int Page { get; set; }
      public int PageSize { get; set; }
      public int TotalCount { get; set; }
      public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
      public bool HasNextPage => Page < TotalPages;
      public bool HasPrevPage => Page > 1;
}