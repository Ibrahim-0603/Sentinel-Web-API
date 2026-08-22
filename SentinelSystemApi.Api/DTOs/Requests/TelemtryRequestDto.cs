using System.ComponentModel.DataAnnotations;

namespace SentinelSystemApi.Api.DTOs;

public class TelemetryRequestDto
{
      [Required(ErrorMessage = "PIR state required")]
      public bool Pir { get; set; }

      [Required(ErrorMessage = "Temperature required")]
      [Range(-30, 999)]
      public Decimal TemperatureC { get; set; }

      [Required(ErrorMessage = "Humidity required")]
      [Range(0, 100)]
      public Decimal Humidity { get; set; }
}