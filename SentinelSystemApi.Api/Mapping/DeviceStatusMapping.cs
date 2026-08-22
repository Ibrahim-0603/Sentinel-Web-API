using AutoMapper;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.Mapping;

public class DeviceStatusMapping : Profile
{
    public DeviceStatusMapping()
    {
        CreateMap<DeviceStatus, DeviceStatusResponseDto>()
        .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.Device.Name))
        .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.Mode.ToString()));

    }
}