using Microsoft.EntityFrameworkCore;
using SentinelSystemApi.Api.Data;
using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Models.Filters;

namespace SentinelSystemApi.Api.Repositories;

public class TelemetryRepository : ITelemetryRepository
{
      private readonly AppDbContext _context;
      public TelemetryRepository(AppDbContext context)
      {
            _context = context;
      }
      public async Task<PagedResult<Telemetry>> Query(TelemetryFilterParams filterParams)
      {
            var query = _context.Telemetries.AsNoTracking().AsQueryable();
            if (filterParams.Pir is not null) query = query.Where(t => t.Pir == filterParams.Pir);
            if (filterParams.MinTemperatureC is not null)
            {
                  query = query.Where(t => t.TemperatureC >= filterParams.MinTemperatureC);
            }
            if (filterParams.MaxTemperatureC is not null)
            {
                  query = query.Where(t => t.TemperatureC <= filterParams.MaxTemperatureC);
            }
            if (filterParams.MinHumidity is not null)
            {
                  query = query.Where(t => t.Humidity >= filterParams.MinHumidity);
            }
            if (filterParams.MaxHumidity is not null)
            {
                  query = query.Where(t => t.Humidity <= filterParams.MaxHumidity);
            }
            if (filterParams.From is not null) query = query.Where(t => t.Timestamp >= filterParams.From);
            if (filterParams.To is not null) query = query.Where(t => t.Timestamp <= filterParams.To);

            if (filterParams.SortBy == "Timestamp")
            {
                  query = filterParams.Order == "asc" ? query.OrderBy(t => t.Timestamp) : query.OrderByDescending(t => t.Timestamp);
            }
            else if (filterParams.SortBy == "TemperatureC")
            {
                  query = filterParams.Order == "asc" ? query.OrderBy(t => t.TemperatureC) : query.OrderByDescending(t => t.TemperatureC);
            }
            else if (filterParams.SortBy == "Humidity")
            {
                  query = filterParams.Order == "asc" ? query.OrderBy(t => t.Humidity) : query.OrderByDescending(t => t.Humidity);
            }

            var totalCount = await query.CountAsync();
            query = query.Skip((filterParams.Page - 1) * filterParams.PageSize).Take(filterParams.PageSize);
            var readings = await query.ToListAsync();

            return new PagedResult<Telemetry>
            {
                  Data = readings,
                  Page = filterParams.Page,
                  PageSize = filterParams.PageSize,
                  TotalCount = totalCount,
            };
      }

      public async Task<Telemetry?> GetById(int id) => await _context.Telemetries.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

      public async Task<Telemetry> AddTelemetry(Telemetry telemetry)
      {
            _context.Telemetries.Add(telemetry);
            await _context.SaveChangesAsync();
            return telemetry;
      }

      public async Task DeleteTelemetry(int id)
      {
            var current = await GetById(id);
            _context.Telemetries.Remove(current);
            await _context.SaveChangesAsync();
      }
}