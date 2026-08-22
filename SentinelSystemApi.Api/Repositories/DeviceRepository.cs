using Microsoft.EntityFrameworkCore;
using SentinelSystemApi.Api.Data;
using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Models.Filters;

namespace SentinelSystemApi.Api.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly AppDbContext _context;
    public DeviceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Device>> Query(DeviceFilterParams filterParams)
    {
        var query = _context.Devices.Include(d =>d.Owner).AsNoTracking().AsQueryable();
        if (filterParams.Id is not null) query = query.Where(d => d.Id == filterParams.Id);
        if (filterParams.Name is not null) query = query.Where(d => d.Name == filterParams.Name);
        if (filterParams.OwnerId is not null) query = query.Where(d => d.OwnerId == filterParams.OwnerId);
        if (filterParams.OwnerName is not null) query = query.Where(d => d.Owner.Username == filterParams.OwnerName);

        var sortBy = filterParams.SortBy?.Trim().ToLowerInvariant();
        var order = filterParams.Order?.Trim().ToLowerInvariant();

        if (sortBy == "createdat") query = order == "asc" ? query.OrderBy(d => d.CreatedAt) : query.OrderByDescending(d => d.CreatedAt);

        if (sortBy == "name") query = order == "asc" ? query.OrderBy(d => d.Name) : query.OrderByDescending(d => d.Name);

        var totalCount = await query.CountAsync();
        query = query.Skip((filterParams.Page - 1) * filterParams.PageSize).Take(filterParams.PageSize);
        var devices = await query.ToListAsync();

        return new PagedResult<Device>
        {
            Data = devices,
            Page = filterParams.Page,
            PageSize = filterParams.PageSize,
            TotalCount = totalCount
        };

    }

    public async Task<Device?> GetById(int id) => await _context.Devices.Include(d => d.Owner).AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
    public async Task<Device?> GetByName(string name) => await _context.Devices.Include(d =>d.Owner).AsNoTracking().FirstOrDefaultAsync(d => d.Name == name);

    public async Task<Device> Create(Device device)
    {
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
        return device;
    }

    public async Task<Device> Update(Device device)
    {
        _context.Devices.Update(device);
        await _context.SaveChangesAsync();
        return device;
    }

    public async Task Delete(Device device)
    {
        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();
    }
}