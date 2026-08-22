using SentinelSystemApi.Api.Enums;

namespace SentinelSystemApi.Api.DTOs;

public class UpdateUserRequestDto
{
	public string? Username { get; set; } = string.Empty;
	public UserRole? Role { get; set; }
}