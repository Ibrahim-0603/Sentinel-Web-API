using System.ComponentModel.DataAnnotations;

namespace SentinelSystemApi.Api.DTOs;

public class DeviceRequestDto
{
    [Required]
    [MaxLength(100)]
    public string Name {get;set;} = string.Empty;
}