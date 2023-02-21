using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IDataRepository<Appointment> _appointmentRepository;
        private IAppointmentService _appointmentService;

        public AppointmentController(IDataRepository<Appointment> appointmentRepository, IAppointmentService appointmentService)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentService = appointmentService;
        }

        // GET: api/appointment
        [HttpGet]
        public ActionResult<PaginationResponseModel<Appointment>> Get([FromQuery] FilterModel filter)
        {
            try
            {
                var result = _appointmentService.GetAppointments(filter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/appointment/5
        [HttpGet("{appointmentId}")]
        public IActionResult Get(int appointmentId)
        {
            try
            {
                Appointment appointment = _appointmentRepository.Get(appointmentId);

                if (appointment == null)
                {
                    return NotFound();
                }
                return Ok(appointment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/appointment
        [HttpPost]
        public IActionResult Post([FromBody] AppointmentModel appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _appointmentRepository.Add(appointment.TransformToDto());
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

        // PUT api/appointment/5
        [HttpPut("{appointmentId}")]
        public IActionResult Put(int appointmentId, [FromBody] AppointmentModel appointment)
        {
            if (ModelState.IsValid)
            {
                Appointment appointmentToUpdate = _appointmentRepository.Get(appointmentId);

                if (appointmentToUpdate == null)
                {
                    return BadRequest("Appointment doesnt exist");

                }
                _appointmentRepository.Update(appointmentToUpdate, appointment.TransformToDto());
                return Ok("Successfully updated");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/appointment/5
        [HttpDelete("{appointmentId}")]
        public IActionResult Delete(int appointmentId)
        {
            Appointment appointmentToDelete = _appointmentRepository.Get(appointmentId);
            if (appointmentToDelete == null)
            {
                return BadRequest("Appointment doesnt exist");

            }
            _appointmentRepository.Delete(appointmentToDelete);
            return Ok("Successfully Deleted");
        }
    }
}

