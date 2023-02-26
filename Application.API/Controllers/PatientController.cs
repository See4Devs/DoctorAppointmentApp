using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private IPatientService _patientService;
        private IMapper _mapper;

        public PatientController(IPatientRepository patientRepository, IPatientService patientService, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _patientService = patientService;
            _mapper = mapper;
        }

        // GET: api/patient
        [HttpGet]
        public async Task<ActionResult<PaginationResponseDto<PatientDto>>> Get([FromQuery] FilterDto filter)
        {
            try
            {
                var result = await _patientService.GetPatientsAsync(filter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/patient/5
        [HttpGet("{patientId}")]
        public async Task<IActionResult> Get(int patientId)
        {
            try
            {
                Patient patient = await _patientRepository.GetAsync(patientId);

                if (patient == null)
                {
                    return NotFound();
                }
                var result = _mapper.Map<PatientDto>(patient);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/patient/5/appointments
        [HttpGet("{patientId}/appointments")]
        public async Task<IActionResult> GetAppointments([FromQuery] DateFilterDto filter, int patientId)
        {
            try
            {
                Patient patient = await _patientRepository.GetPatientAppointmentsAsync(filter, patientId);

                if (patient == null)
                {
                    return NotFound();
                }
                var result = _mapper.Map<PatientAppointmentsDto>(patient);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/patient
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientBodyDto patient)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Patient patientEntity = _mapper.Map<Patient>(patient);
                    string response = await _patientRepository.AddAsync(patientEntity);
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

        // PUT api/patient/5
        [HttpPut("{patientId}")]
        public async Task<IActionResult> Put(int patientId, [FromBody] PatientBodyDto patient)
        {
            if (ModelState.IsValid)
            {
                Patient patientToUpdate = await _patientRepository.GetAsync(patientId);

                if (patientToUpdate == null)
                {
                    return BadRequest("Patient doesnt exist");

                }
                Patient patientEntity = _mapper.Map<Patient>(patient);
                string response = await _patientRepository.UpdateAsync(patientToUpdate, patientEntity);
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/patient/5
        [HttpDelete("{patientId}")]
        public async Task<IActionResult> Delete(int patientId)
        {
            Patient patientToDelete = await _patientRepository.GetAsync(patientId);
            if (patientToDelete == null)
            {
                return BadRequest("Patient doesnt exist");

            }
            string response = await _patientRepository.DeleteAsync(patientToDelete);
            return Ok(response);
        }
    }
}

