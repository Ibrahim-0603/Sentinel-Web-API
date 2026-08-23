using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.DTOs;

public class EventResponseDto
{
    public int Id { get; set; }
    public string? ClipPath { get; set; }
    public string? Notes { get; set; }
    public string? EventType { get; set; }
    public DateTime Timestamp { get; set; }
    public int? TelemetryId { get; set; }
}