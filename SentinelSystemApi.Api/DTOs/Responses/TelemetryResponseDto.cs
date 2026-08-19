namespace SentinelSystemApi.Api.DTOs;

public class TelemetryResponseDto
{
      public int Id { get; set; }
      public bool Pir { get; set; }
      public Decimal TemperatureC { get; set; }
      public Decimal Humidity { get; set; }
      public DateTime Timestamp { get; set; }

}