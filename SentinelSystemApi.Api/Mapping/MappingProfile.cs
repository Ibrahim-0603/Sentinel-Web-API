using AutoMapper;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.Mapping;

public class MappingProfile: Profile
{
      public MappingProfile()
      {
            CreateMap<Telemetry, TelemetryResponseDto>();
            CreateMap<TelemetryRequestDto, Telemetry >()
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
      }
}