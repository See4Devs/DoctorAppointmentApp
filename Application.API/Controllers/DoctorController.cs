using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private IDoctorService _doctorService;
        private IMapper _mapper;

        public DoctorController(IDoctorRepository doctorRepository, IDoctorService doctorService, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _doctorService = doctorService;
            _mapper = mapper;
        }

        // GET: api/doctor
        [HttpGet]
        public async Task<ActionResult<PaginationResponseDto<DoctorDto>>> Get([FromQuery] FilterDto filter)
        {
            try
            {
                var result = await _doctorService.GetDoctorsAsync(filter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/doctor/5
        [HttpGet("{doctorId}")]
        public async Task<IActionResult> Get(int doctorId)
        {
            try
            {
                Doctor doctor = await _doctorRepository.GetAsync(doctorId);
                if (doctor == null)
                {
                    return NotFound();
                }
                var result = _mapper.Map<DoctorDto>(doctor);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/doctor/5/appointments
        [HttpGet("{doctorId}/appointments")]
        public async Task<IActionResult> GetDoctorAppointments([FromQuery] DateFilterDto filter, int doctorId)
        {
            try
            {
                Doctor doctor = await _doctorRepository.GetDoctorAppointmentsAsync(filter, doctorId);
                if (doctor == null)
                {
                    return NotFound();
                }
                var result = _mapper.Map<DoctorAppointmentsDto>(doctor);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/doctor
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DoctorBodyDto doctor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Doctor doctorEntity = _mapper.Map<Doctor>(doctor);
                    string response = await _doctorRepository.AddAsync(doctorEntity);
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

        // PUT api/doctor/5
        [HttpPut("{doctorId}")]
        public async Task<IActionResult> Put(int doctorId, [FromBody] DoctorBodyDto doctor)
        {
            if (ModelState.IsValid)
            {
                Doctor doctorToUpdate = await _doctorRepository.GetAsync(doctorId);

                if (doctorToUpdate == null)
                {
                    return BadRequest("Doctor doesnt exist");

                }
                Doctor doctorEntity = _mapper.Map<Doctor>(doctor);
                string response = await _doctorRepository.UpdateAsync(doctorToUpdate, doctorEntity);
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/doctor/5
        [HttpDelete("{doctorId}")]
        public async Task<IActionResult> Delete(int doctorId)
        {
            Doctor doctorToDelete = await _doctorRepository.GetAsync(doctorId);
            if (doctorToDelete == null)
            {
                return BadRequest("Doctor doesnt exist");

            }
            string response = await _doctorRepository.DeleteAsync(doctorToDelete);
            return Ok(response);
        }
    }
}

