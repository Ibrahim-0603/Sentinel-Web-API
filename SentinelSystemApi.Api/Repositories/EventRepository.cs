using Microsoft.EntityFrameworkCore;
using SentinelSystemApi.Api.Data;
using SentinelSystemApi.Api.Enums;
using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Models.Filters;

namespace SentinelSystemApi.Api.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;
    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Event>> Query(EventFilterParams filterParams)
    {
        var query = _context.Events.Include(e => e.Telemetry).ThenInclude(t => t.Device).AsNoTracking().AsQueryable();

        if (filterParams.OwnerId is not null) query = query.Where(e => e.Telemetry != null && e.Telemetry.Device.OwnerId == filterParams.OwnerId);
        if (filterParams.From is not null) query = query.Where(e => e.Timestamp >= filterParams.From);
        if (filterParams.To is not null) query = query.Where(e => e.Timestamp <= filterParams.To);
        var eventType = filterParams.EventType?.Trim().ToLowerInvariant();
        if (eventType == "motiondetected") query = query.Where(e => e.EventType == Enum.Parse<EventType>(eventType));
        if (eventType == "pirread") query = query.Where(e => e.EventType == Enum.Parse<EventType>(eventType));
        if (eventType == "modechanged") query = query.Where(e => e.EventType == Enum.Parse<EventType>(eventType));

        string? sortBy = filterParams.SortBy?.Trim().ToLowerInvariant();
        string? order = filterParams.Order?.Trim().ToLowerInvariant();

        if (sortBy == "timestamp") query = order == "asc" ? query.OrderBy(e => e.Timestamp) : query.OrderByDescending(e => e.Timestamp);
        if (sortBy == eventType) query = order == "asc" ? query.OrderBy(e => e.EventType) : query.OrderByDescending(e => e.EventType);

        var totalCount = await query.CountAsync();
        query = query.Skip((filterParams.Page - 1) * filterParams.PageSize).Take(filterParams.PageSize);
        var events = await query.ToListAsync();

        return new PagedResult<Event>
        {
            Data = events,
            Page = filterParams.Page,
            PageSize = filterParams.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<Event?> GetById(int id) => await _context.Events.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

    public async Task<Event> Create(Event evnt)
    {
        _context.Events.Add(evnt);
        await _context.SaveChangesAsync();
        return evnt;
    }

    public async Task<Event> Update(Event evnt)
    {
        _context.Events.Update(evnt);
        await _context.SaveChangesAsync();
        return evnt;
    }

    public async Task Delete(Event evnt)
    {
        _context.Events.Remove(evnt);
        await _context.SaveChangesAsync();
    }
}