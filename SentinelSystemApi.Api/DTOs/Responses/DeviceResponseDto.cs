namespace SentinelSystemApi.Api.DTOs;

public class DeviceResponseDto
{
    public int Id {get;set;}
    public string Name {get;set;} =string.Empty;
    public DateTime CreatedAt {get;set;}
    public int OwnerId {get;set;}
    public string OwnerName {get;set;} = string.Empty;
}