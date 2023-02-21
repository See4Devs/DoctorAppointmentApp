using Application.Domain.Dao;
using Application.Domain.Dto;

namespace Application.Domain.Interfaces
{
    public interface IEventService
    {
        PaginationResponseModel<Event> GetEvents(FilterModel filter);
    }
}