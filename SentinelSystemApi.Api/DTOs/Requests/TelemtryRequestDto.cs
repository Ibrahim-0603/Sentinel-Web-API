using System.ComponentModel.DataAnnotations;

namespace SentinelSystemApi.Api.DTOs;

public class TelemetryRequestDto
{
      [Required]
      public bool Pir { get; set; }
      [Required]
      public Decimal TemperatureC { get; set; }
      [Required]
      public Decimal Humidity { get; set; }
}