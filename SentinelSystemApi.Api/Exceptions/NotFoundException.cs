namespace SentinelSystemApi.Api.Exceptions;

public class NotFoundException : Exception
{
      public NotFoundException(int id, string item) : base($"{item} with ID {id} not found") { }
      public NotFoundException(int id) : base($"Item with ID {id} not found") { }
}