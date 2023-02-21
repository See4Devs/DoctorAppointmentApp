using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IDataRepository<Patient> _patientRepository;
        private IPatientService _patientService;

        public PatientController(IDataRepository<Patient> patientRepository, IPatientService patientService)
        {
            _patientRepository = patientRepository;
            _patientService = patientService;
        }

        // GET: api/patient
        [HttpGet]
        public ActionResult<PaginationResponseModel<Patient>> Get([FromQuery] FilterModel filter)
        {
            try
            {
                var result = _patientService.GetPatients(filter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/patient/5
        [HttpGet("{patientId}")]
        public IActionResult Get(int patientId)
        {
            try
            {
                Patient Patient = _patientRepository.Get(patientId);

                if (Patient == null)
                {
                    return NotFound();
                }
                return Ok(Patient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/patient
        [HttpPost]
        public IActionResult Post([FromBody] PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _patientRepository.Add(patient.TransformToDto());
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

        // PUT api/patient/5
        [HttpPut("{patientId}")]
        public IActionResult Put(int patientId, [FromBody] PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                Patient patientToUpdate = _patientRepository.Get(patientId);

                if (patientToUpdate == null)
                {
                    return BadRequest("Patient doesnt exist");

                }
                _patientRepository.Update(patientToUpdate, patient.TransformToDto());
                return Ok("Successfully updated");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/patient/5
        [HttpDelete("{patientId}")]
        public IActionResult Delete(int patientId)
        {
            Patient patientToDelete = _patientRepository.Get(patientId);
            if (patientToDelete == null)
            {
                return BadRequest("Patient doesnt exist");

            }
            _patientRepository.Delete(patientToDelete);
            return Ok("Successfully Deleted");
        }
    }
}

