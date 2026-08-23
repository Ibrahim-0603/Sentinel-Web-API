using System.ComponentModel.DataAnnotations;

namespace SentinelSystemApi.Api.DTOs;

public class UpdateEventRequestDto
{
    [Required]
    [MaxLength(1000)]
    public string Notes { get; set; } = string.Empty;
}