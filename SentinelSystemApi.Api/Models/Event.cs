using SentinelSystemApi.Api.Enums;

namespace SentinelSystemApi.Api.Models;

public class Event
{
	public int Id { get; set; }
	public string? ClipPath { get; set; } = string.Empty;
	public string? Notes { get; set; } = string.Empty;
	public EventType eventType { get; set; }
	public DateTime timeStamp { get; set; }

	public int? TelemetryId { get; set; }
	public Telemetry? Telemetry { get; set; }
}