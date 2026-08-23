using SentinelSystemApi.Api.Enums;

namespace SentinelSystemApi.Api.Models;

public class Event
{
	public int Id { get; set; }
	public string? ClipPath { get; set; }
	public string? Notes { get; set; }
	public EventType EventType { get; set; }
	public DateTime Timestamp { get; set; }

	public int? TelemetryId { get; set; }
	public Telemetry? Telemetry { get; set; }
}