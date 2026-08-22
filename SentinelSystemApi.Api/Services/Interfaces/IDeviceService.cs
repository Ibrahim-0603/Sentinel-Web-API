using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models.Filters;

namespace SentinelSystemApi.Api.Services;

public interface IDeviceService
{
    Task<PagedResult<DeviceResponseDto>> GetAllDevices(DeviceFilterParams filterParams, int callerId, bool isAdmin);
    Task<DeviceResponseDto?> GetDeviceById(int id, int callerId, bool isAdmin);
    Task<DeviceResponseDto?> GetDeviceByName(string name, int callerId, bool isAdmin);
    Task<DeviceResponseDto> CreateDevice(DeviceRequestDto requestDto, int callerId);
    Task<DeviceResponseDto> UpdateDevice(int id, DeviceRequestDto requestDto, int callerId, bool isAdmin);
    Task DeleteDevice(int id, int callerId, bool isAdmin);

}