using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDataRepository<Doctor> _doctorRepository;
        private IDoctorService _doctorService;

        public DoctorController(IDataRepository<Doctor> doctorRepository, IDoctorService doctorService)
        {
            _doctorRepository = doctorRepository;
            _doctorService = doctorService;
        }

        // GET: api/doctor
        [HttpGet]
        public ActionResult<PaginationResponseModel<Doctor>> Get([FromQuery] FilterModel filter)
        {
            try
            {
                var result = _doctorService.GetDoctors(filter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/doctor/5
        [HttpGet("{doctorId}")]
        public IActionResult Get(int doctorId)
        {
            try
            {
                Doctor Doctor = _doctorRepository.Get(doctorId);

                if (Doctor == null)
                {
                    return NotFound();
                }
                return Ok(Doctor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/doctor
        [HttpPost]
        public IActionResult Post([FromBody] DoctorModel doctor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _doctorRepository.Add(doctor.TransformToDto());
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

        // PUT api/doctor/5
        [HttpPut("{doctorId}")]
        public IActionResult Put(int doctorId, [FromBody] DoctorModel doctor)
        {
            if (ModelState.IsValid)
            {
                Doctor doctorToUpdate = _doctorRepository.Get(doctorId);

                if (doctorToUpdate == null)
                {
                    return BadRequest("Doctor doesnt exist");

                }
                _doctorRepository.Update(doctorToUpdate, doctor.TransformToDto());
                return Ok("Successfully updated");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/doctor/5
        [HttpDelete("{doctorId}")]
        public IActionResult Delete(int doctorId)
        {
            Doctor doctorToDelete = _doctorRepository.Get(doctorId);
            if (doctorToDelete == null)
            {
                return BadRequest("Doctor doesnt exist");

            }
            _doctorRepository.Delete(doctorToDelete);
            return Ok("Successfully Deleted");
        }
    }
}

