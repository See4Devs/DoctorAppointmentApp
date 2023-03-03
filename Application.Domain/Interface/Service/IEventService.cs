using Application.Domain.Dto;

namespace Application.Domain.Interfaces
{
    public interface IEventService
    {
        Task<PaginationResponseDto<EventDto>> GetEventsAsync(FilterDto filter);
    }
}