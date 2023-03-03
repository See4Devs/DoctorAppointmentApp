using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private IEventService _eventService;
        private IMapper _mapper;

        public EventController(IEventRepository eventRepository, IEventService eventService, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _eventService = eventService;
            _mapper = mapper;
        }

        // GET: api/event
        [HttpGet]
        public async Task<ActionResult<PaginationResponseDto<EventDto>>> Get([FromQuery] FilterDto filter)
        {
            try
            {
                var result = await _eventService.GetEventsAsync(filter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/event/5
        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(int eventId)
        {
            try
            {
                Event eventObj = await _eventRepository.GetAsync(eventId);

                if (eventObj == null)
                {
                    return NotFound();
                }
                var result = _mapper.Map<EventDto>(eventObj);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/event
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EventBodyDto eventObj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Event eventEntity = _mapper.Map<Event>(eventObj);
                    string response = await _eventRepository.AddAsync(eventEntity);
                    return Ok(response);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // PUT api/event/5
        [HttpPut("{eventId}")]
        public async Task<IActionResult> Put(int eventId, [FromBody] EventBodyDto eventObj)
        {
            if (ModelState.IsValid)
            {
                Event eventToUpdate = await _eventRepository.GetAsync(eventId);

                if (eventToUpdate == null)
                {
                    return BadRequest("Event doesnt exist");

                }
                Event eventEntity = _mapper.Map<Event>(eventObj);
                string response = await _eventRepository.UpdateAsync(eventToUpdate, eventEntity);
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/event/5
        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete(int eventId)
        {
            Event eventToDelete = await _eventRepository.GetAsync(eventId);
            if (eventToDelete == null)
            {
                return BadRequest("Event doesnt exist");

            }
            string response = await _eventRepository.DeleteAsync(eventToDelete);
            return Ok(response);
        }
    }
}

