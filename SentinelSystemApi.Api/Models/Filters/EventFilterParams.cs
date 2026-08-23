namespace SentinelSystemApi.Api.Models.Filters;

public class EventFilterParams : PaginationParams
{
    public string? EventType { get; set; } = string.Empty;
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? OwnerId { get; set; }
    public string? SortBy { get; set; } = "Timestamp";
    public string? Order { get; set; } = "asc";
}