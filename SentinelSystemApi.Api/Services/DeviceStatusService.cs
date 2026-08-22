using AutoMapper;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Enums;
using SentinelSystemApi.Api.Exceptions;
using SentinelSystemApi.Api.Repositories;

namespace SentinelSystemApi.Api.Services;

public class DeviceStatusService : IDeviceStatusService
{
    private readonly IDeviceStatusRepository _deviceStatusRepository;
    private readonly IMapper _mapper;

    public DeviceStatusService(IDeviceStatusRepository deviceStatusRepository, IMapper mapper)
    {
        _deviceStatusRepository = deviceStatusRepository;
        _mapper = mapper;
    }

    public async Task<DeviceStatusResponseDto> GetByDeviceId(int deviceId, int callerId, bool isAdmin)
    {
        var status = await _deviceStatusRepository.GetByDeviceId(deviceId);
        if (status is null) throw new NotFoundException(deviceId, "Device");
        if (!isAdmin && status.Device.OwnerId != callerId) throw new ForbiddenException("You do not have access to this device's status.");
        return _mapper.Map<DeviceStatusResponseDto>(status);
    }
    public async Task<DeviceStatusResponseDto> GetByDeviceName(string deviceName, int callerId, bool isAdmin)
    {
        var status = await _deviceStatusRepository.GetByDeviceName(deviceName);
        if (status is null) throw new NotFoundException(deviceName, "Device");
        if (!isAdmin && status.Device.OwnerId != callerId) throw new ForbiddenException("You do not have access to this device's status.");
        return _mapper.Map<DeviceStatusResponseDto>(status);
    }
    public async Task<DeviceStatusResponseDto> UpdateStatusById(int deviceId, UpdateDeviceStatusRequestDto requestDto, int callerId, bool isAdmin)
    {
        var status = await _deviceStatusRepository.GetByDeviceId(deviceId);
        if (status is null) throw new NotFoundException(deviceId, "Device");
        if (!isAdmin && status.Device.OwnerId != callerId) throw new ForbiddenException("You cannot modify this device's status.");

        status.Mode = Enum.Parse<DeviceMode>(requestDto.Mode);
        var updated = await _deviceStatusRepository.Update(status);
        return _mapper.Map<DeviceStatusResponseDto>(updated);
    }
    public async Task<DeviceStatusResponseDto> UpdateStatusByName(string deviceName, UpdateDeviceStatusRequestDto requestDto, int callerId, bool isAdmin)
    {
        var status = await _deviceStatusRepository.GetByDeviceName(deviceName);
        if (status is null) throw new NotFoundException(deviceName, "Device");
        if (!isAdmin && status.Device.OwnerId != callerId) throw new ForbiddenException("You cannot modify this device's status.");

        status.Mode = Enum.Parse<DeviceMode>(requestDto.Mode);
        var updated = await _deviceStatusRepository.Update(status);
        return _mapper.Map<DeviceStatusResponseDto>(updated);
    }

    public async Task DeleteStatus(int deviceId)
    {
        var status = await _deviceStatusRepository.GetByDeviceId(deviceId);
        if (status is null) throw new NotFoundException(deviceId, "Device");
        await _deviceStatusRepository.Delete(status);
    }
}