namespace SentinelSystemApi.Api.Models;

public class Device
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public int OwnerId { get; set; }
	public User Owner { get; set; } = null!;
	public DeviceStatus DeviceStatus { get; set; } = null!;
	public ICollection<Telemetry> TelemetryReadings { get; set; } = new List<Telemetry>();
}