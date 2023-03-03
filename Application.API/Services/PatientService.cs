using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;

namespace Application.API.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PaginationResponseDto<PatientDto>> GetPatientsAsync(FilterDto filter)
        {
            IEnumerable<Patient> patients = await _patientRepository.GetAllAsync();

            IEnumerable<PatientDto> filteredPatients = patients.Where(p => p.Name.StartsWith(filter.searchText ?? String.Empty, StringComparison.InvariantCultureIgnoreCase))
                .Skip((filter.page - 1) * filter.limit)
                .Take(filter.limit).Select(p => new PatientDto
                {
                    PatientId = p.PatientId,
                    Name = p.Name,
                    Email = p.Email,
                    Address = p.Address,
                    DateOfBirth = p.DateOfBirth
                });

            var result = new PaginationResponseDto<PatientDto>();

            result.Items = filteredPatients;

            return result;
        }
    }
}

