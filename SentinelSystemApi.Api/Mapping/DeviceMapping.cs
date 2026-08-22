using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.Mapping;

public class DeviceMapping : Profile
{
    public DeviceMapping()
    {
        CreateMap<Device, DeviceResponseDto>().ForMember(dest => dest.OwnerName, opt => opt.MapFrom(d => d.Owner.Username));

        CreateMap<DeviceRequestDto, Device>().ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.OwnerId, opt => opt.Ignore());
    }
}