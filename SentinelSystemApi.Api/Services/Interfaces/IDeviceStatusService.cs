using SentinelSystemApi.Api.DTOs;

namespace SentinelSystemApi.Api.Services;

public interface IDeviceStatusService
{
    public Task<DeviceStatusResponseDto> GetByDeviceId(int deviceId, int callerId, bool isAdmin);
    public Task<DeviceStatusResponseDto> GetByDeviceName(string deviceName, int callerId, bool isAdmin);
    public Task<DeviceStatusResponseDto> UpdateStatusById(int deviceId, UpdateDeviceStatusRequestDto requestDto, int callerId, bool isAdmin);
    public Task<DeviceStatusResponseDto> UpdateStatusByName(string deviceName, UpdateDeviceStatusRequestDto requestDto, int callerId, bool isAdmin);
    public Task DeleteStatus(int deviceId);
}