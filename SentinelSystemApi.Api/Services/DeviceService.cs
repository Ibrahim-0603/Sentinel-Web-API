using AutoMapper;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Exceptions;
using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Models.Filters;
using SentinelSystemApi.Api.Repositories;

namespace SentinelSystemApi.Api.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IDeviceStatusRepository _deviceStatusRepository;
    private readonly IMapper _mapper;

    public DeviceService(IDeviceRepository deviceRepository, IDeviceStatusRepository deviceStatusRepository, IMapper mapper)
    {
        _deviceRepository = deviceRepository;
        _deviceStatusRepository = deviceStatusRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<DeviceResponseDto>> GetAllDevices(DeviceFilterParams filterParams, int callerId, bool isAdmin)
    {
        if (!isAdmin) filterParams.OwnerId = callerId;
        var result = await _deviceRepository.Query(filterParams);

        return new PagedResult<DeviceResponseDto>
        {
            Data = _mapper.Map<IEnumerable<DeviceResponseDto>>(result.Data),
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<DeviceResponseDto?> GetDeviceById(int id, int callerId, bool isAdmin)
    {
        var device = await _deviceRepository.GetById(id);
        if (device is null) throw new NotFoundException(id, "Device");
        if (!isAdmin && device.OwnerId != callerId) throw new ForbiddenException("You do not have access to this device");

        return _mapper.Map<DeviceResponseDto>(device);
    }
    public async Task<DeviceResponseDto?> GetDeviceByName(string name, int callerId, bool isAdmin)
    {
        var device = await _deviceRepository.GetByName(name);
        if (device is null) throw new NotFoundException(name, "Device");
        if (!isAdmin && device.OwnerId != callerId) throw new ForbiddenException("You do not have access to this device");

        return _mapper.Map<DeviceResponseDto>(device);
    }

    public async Task<DeviceResponseDto> CreateDevice(DeviceRequestDto requestDto, int callerId)
    {
        var device = _mapper.Map<Device>(requestDto);
        device.OwnerId = callerId;
        device.CreatedAt = DateTime.Now;

        var created = await _deviceRepository.Create(device);
        await _deviceStatusRepository.Create(new DeviceStatus
        {
            DeviceId = created.Id,
            Mode = Enums.DeviceMode.Patrolling,
            LastSeenAt = DateTime.Now,
            PanAngle = 0,
            TiltAngle = 0
        });
        return _mapper.Map<DeviceResponseDto>(created);
    }

    public async Task<DeviceResponseDto> UpdateDevice(int id, DeviceRequestDto requestDto, int callerId, bool isAdmin)
    {
        var device = await _deviceRepository.GetById(id);
        if (device is null) throw new NotFoundException(id, "Device");
        if (device.OwnerId != callerId && !isAdmin) throw new ForbiddenException("You do not have access to this device");

        if (requestDto.Name is not null) device.Name = requestDto.Name;

        var updated = await _deviceRepository.Update(device);
        return _mapper.Map<DeviceResponseDto>(updated);
    }

    public async Task DeleteDevice(int id, int callerId, bool isAdmin)
    {
        var device = await _deviceRepository.GetById(id);
        if (device is null) throw new NotFoundException(id, "Device");
        if (!isAdmin && device.OwnerId != callerId) throw new ForbiddenException("You do not have access to this device");

        await _deviceRepository.Delete(device);
    }
}