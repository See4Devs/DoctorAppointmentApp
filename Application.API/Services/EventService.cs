using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;

namespace Application.API.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<PaginationResponseDto<EventDto>> GetEventsAsync(FilterDto filter)
        {
            IEnumerable<Event> events = await _eventRepository.GetAllAsync();

            IEnumerable<EventDto> filteredEvents = events.Where(p => p.Name.StartsWith(filter.searchText ?? String.Empty, StringComparison.InvariantCultureIgnoreCase))
                .Skip((filter.page - 1) * filter.limit)
                .Take(filter.limit).Select(e => new EventDto
                {
                    EventId = e.EventId,
                    Name = e.Name,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime
                });

            var result = new PaginationResponseDto<EventDto>();

            result.Items = filteredEvents;

            return result;
        }
    }
}

