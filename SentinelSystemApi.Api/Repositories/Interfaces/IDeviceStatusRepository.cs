using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.Repositories;

public interface IDeviceStatusRepository
{
    public Task<DeviceStatus?> GetByDeviceId(int deviceId);
    public Task<DeviceStatus?> GetByDeviceName(string deviceName);
    public Task<DeviceStatus> Create(DeviceStatus status);
    public Task<DeviceStatus> Update(DeviceStatus status);
    public Task Delete(DeviceStatus status);

}