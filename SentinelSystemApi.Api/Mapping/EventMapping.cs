using AutoMapper;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.Mapping;

public class EventMapping : Profile
{
    public EventMapping()
    {
        CreateMap<Event, EventResponseDto>()
        .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType.ToString()));
    }
}