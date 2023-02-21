using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IDataRepository<Event> _eventRepository;
        private IEventService _eventService;

        public EventController(IDataRepository<Event> eventRepository, IEventService eventService)
        {
            _eventRepository = eventRepository;
            _eventService = eventService;
        }

        // GET: api/event
        [HttpGet]
        public ActionResult<PaginationResponseModel<Event>> Get([FromQuery] FilterModel filter)
        {
            try
            {
                var result = _eventService.GetEvents(filter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/event/5
        [HttpGet("{eventId}")]
        public IActionResult Get(int eventId)
        {
            try
            {
                Event eventObj = _eventRepository.Get(eventId);

                if (eventObj == null)
                {
                    return NotFound();
                }
                return Ok(eventObj);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/event
        [HttpPost]
        public IActionResult Post([FromBody] EventModel eventObj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _eventRepository.Add(eventObj.TransformToDto());
                    return Ok("Successfully created");
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
        public IActionResult Put(int eventId, [FromBody] EventModel eventObj)
        {
            if (ModelState.IsValid)
            {
                Event eventToUpdate = _eventRepository.Get(eventId);

                if (eventToUpdate == null)
                {
                    return BadRequest("Event doesnt exist");

                }
                _eventRepository.Update(eventToUpdate, eventObj.TransformToDto());
                return Ok("Successfully updated");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/event/5
        [HttpDelete("{eventId}")]
        public IActionResult Delete(int eventId)
        {
            Event eventToDelete = _eventRepository.Get(eventId);
            if (eventToDelete == null)
            {
                return BadRequest("Event doesnt exist");

            }
            _eventRepository.Delete(eventToDelete);
            return Ok("Successfully Deleted");
        }
    }
}

