using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Models.Filters;

namespace SentinelSystemApi.Api.Repositories;

public interface IDeviceRepository
{
    Task<PagedResult<Device>> Query(DeviceFilterParams filterParams);
    Task<Device?> GetById(int id);
    Task<Device?> GetByName(string name);
    Task<Device> Create(Device device);
    Task<Device> Update(Device device);
    Task Delete(Device device);
}