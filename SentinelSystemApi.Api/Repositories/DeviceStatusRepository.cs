using Microsoft.EntityFrameworkCore;
using SentinelSystemApi.Api.Data;
using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.Repositories;

public class DeviceStatusRepository : IDeviceStatusRepository
{
    private readonly AppDbContext _context;

    public DeviceStatusRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DeviceStatus?> GetByDeviceId(int deviceId) => await _context.DeviceStatuses.Include(d => d.Device).AsNoTracking().FirstOrDefaultAsync(ds => ds.DeviceId == deviceId);

    public async Task<DeviceStatus?> GetByDeviceName(string deviceName) => await _context.DeviceStatuses.Include(d => d.Device).AsNoTracking().FirstOrDefaultAsync(ds => ds.Device.Name == deviceName);

    public async Task<DeviceStatus> Create(DeviceStatus status)
    {
        _context.DeviceStatuses.Add(status);
        await _context.SaveChangesAsync();
        return status;
    }

    public async Task<DeviceStatus> Update(DeviceStatus status)
    {
        _context.DeviceStatuses.Update(status);
        await _context.SaveChangesAsync();
        return status;
    }
    public async Task Delete(DeviceStatus status)
    {
        _context.Remove(status);
        await _context.SaveChangesAsync();
    }

}