using SentinelSystemApi.Api.Enums;

namespace SentinelSystemApi.Api.Models;

public class User
{
	public int Id { get; set; }
	public string Username { get; set; } = string.Empty;
	public string PasswordHash { get; set; } = string.Empty;
	public UserRole Role { get; set; }
	public DateTime CreatedAt { get; set; }
	public ICollection<Device> Devices {get;set;} = new List<Device>();
}