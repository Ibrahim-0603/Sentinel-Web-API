namespace SentinelSystemApi.Api.Models.Filters;

public class TelemetryFilterParams : PaginationParams
{
	public int? OwnerId { get; set; }
	public bool? Pir { get; set; }
	public decimal? MinTemperatureC { get; set; }
	public decimal? MaxTemperatureC { get; set; }
	public decimal? MinHumidity { get; set; }
	public decimal? MaxHumidity { get; set; }
	public DateTime? From { get; set; }
	public DateTime? To { get; set; }
	public string? SortBy { get; set; } = "Timestamp";
	public string? Order { get; set; } = "asc";

}