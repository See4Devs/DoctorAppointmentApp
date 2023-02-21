using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;

namespace Application.API.Services
{
    public class EventService : IEventService
    {
        private readonly IDataRepository<Event> _eventRepository;

        public EventService(IDataRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public PaginationResponseModel<Event> GetEvents(FilterModel filter)
        {
            IEnumerable<Event> events = _eventRepository.GetAll();

            IEnumerable<Event> filteredEvents = events.Where(p => p.Name.StartsWith(filter.searchText ?? String.Empty, StringComparison.InvariantCultureIgnoreCase))
                .Skip((filter.page - 1) * filter.limit)
                .Take(filter.limit);

            var result = new PaginationResponseModel<Event>();

            result.Items = filteredEvents;

            return result;
        }
    }
}

