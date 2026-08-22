using AutoMapper;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.Mapping;

public class UserMapping : Profile
{
      public UserMapping()
      {
            CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
            
            CreateMap<UpdateUserRequestDto, User>();
      }
}