namespace SentinelSystemApi.Api.Models.Filters;

public class DeviceFilterParams : PaginationParams
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? OwnerName { get; set; }
    public int? OwnerId { get; set; }
    public string? SortBy { get; set; } = "CreatedAt";
    public string? Order { get; set; } = "asc";
}