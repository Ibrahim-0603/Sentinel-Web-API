using SentinelSystemApi.Api.Enums;

namespace SentinelSystemApi.Api.DTOs;

public class DeviceStatusResponseDto
{
    public int Id { get; set; }
    public string Mode { get; set; } = string.Empty;
    public DateTime LastSeenAt { get; set; }
    public decimal PanAngle { get; set; }
    public decimal TiltAngle { get; set; }
    public int DeviceId { get; set; }
    public string DeviceName { get; set; } = string.Empty;
}