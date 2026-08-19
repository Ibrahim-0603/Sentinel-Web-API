namespace SentinelSystemApi.Api.Models;

public class Telemetry
{
      public int Id { get; set; }
      public bool Pir { get; set; }
      public Decimal TemperatureC { get; set; }
      public Decimal Humidity { get; set; }
      public DateTime Timestamp { get; set; }

      public ICollection<Event> Events { get; set; } = new List<Event>();
}