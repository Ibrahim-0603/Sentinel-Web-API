using System.ComponentModel.DataAnnotations;
using SentinelSystemApi.Api.Enums;

namespace SentinelSystemApi.Api.DTOs;

public class UpdateDeviceStatusRequestDto
{
    [Required]
    [EnumDataType(typeof(DeviceMode))]
    public string Mode {get;set;} = string.Empty;
}