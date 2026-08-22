using SentinelSystemApi.Api.Enums;

namespace SentinelSystemApi.Api.Models;

public class DeviceStatus
{
	public int Id { get; set; }
	public DeviceMode Mode { get; set; }
	public DateTime LastSeenAt { get; set; }
	public decimal PanAngle { get; set; }
	public decimal TiltAngle { get; set; }
	public int DeviceId { get; set; }
	public Device Device { get; set; } = null!;

}