using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private IAppointmentService _appointmentService;
        private IMapper _mapper;

        public AppointmentController(IAppointmentRepository appointmentRepository, IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        // GET: api/appointment
        [HttpGet]
        public async Task<ActionResult<PaginationResponseDto<AppointmentDto>>> Get([FromQuery] FilterDto filter)
        {
            try
            {
                var result = await _appointmentService.GetAppointmentsAsync(filter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/appointment/5
        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> Get(int appointmentId)
        {
            try
            {
                Appointment appointment = await _appointmentRepository.GetAsync(appointmentId);
                if (appointment == null)
                {
                    return NotFound();
                }
                var result = _mapper.Map<AppointmentDetailsDto>(appointment);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/appointment
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AppointmentBodyDto appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Appointment appointmentEntity = _mapper.Map<Appointment>(appointment);
                    string response = await _appointmentRepository.AddAsync(appointmentEntity);
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

        // PUT api/appointment/5
        [HttpPut("{appointmentId}")]
        public async Task<IActionResult> Put(int appointmentId, [FromBody] AppointmentBodyDto appointment)
        {
            if (ModelState.IsValid)
            {
                Appointment appointmentToUpdate = await _appointmentRepository.GetAsync(appointmentId);

                if (appointmentToUpdate == null)
                {
                    return BadRequest("Appointment doesnt exist");

                }
                Appointment appointmentEntity = _mapper.Map<Appointment>(appointment);
                string response = await _appointmentRepository.UpdateAsync(appointmentToUpdate, appointmentEntity);
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/appointment/5
        [HttpDelete("{appointmentId}")]
        public async Task<IActionResult> Delete(int appointmentId)
        {
            Appointment appointmentToDelete = await _appointmentRepository.GetAsync(appointmentId);
            if (appointmentToDelete == null)
            {
                return BadRequest("Appointment doesnt exist");

            }
            string response = await _appointmentRepository.DeleteAsync(appointmentToDelete);
            return Ok(response);
        }
    }
}

