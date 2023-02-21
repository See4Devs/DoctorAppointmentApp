using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;

namespace Application.API.Services
{
    public class PatientService : IPatientService
    {
        private readonly IDataRepository<Patient> _patientRepository;

        public PatientService(IDataRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public PaginationResponseModel<Patient> GetPatients(FilterModel filter)
        {
            IEnumerable<Patient> patients = _patientRepository.GetAll();

            IEnumerable<Patient> filteredPatients = patients.Where(p => p.Name.StartsWith(filter.searchText ?? String.Empty, StringComparison.InvariantCultureIgnoreCase))
                .Skip((filter.page - 1) * filter.limit)
                .Take(filter.limit);

            var result = new PaginationResponseModel<Patient>();

            result.Items = filteredPatients;

            return result;
        }
    }
}

