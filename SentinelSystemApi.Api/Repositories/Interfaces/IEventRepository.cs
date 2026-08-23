using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Models.Filters;

namespace SentinelSystemApi.Api.Repositories;

public interface IEventRepository
{
    Task<PagedResult<Event>> Query(EventFilterParams filterParams);
    Task<Event?> GetById(int id);
    Task<Event> Create(Event evnt);
    Task<Event> Update(Event evnt);
    Task Delete(Event evnt);
}