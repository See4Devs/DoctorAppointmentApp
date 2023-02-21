using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;

namespace Application.API.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDataRepository<Doctor> _doctorRepository;

        public DoctorService(IDataRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public PaginationResponseModel<Doctor> GetDoctors(FilterModel filter)
        {
            IEnumerable<Doctor> doctors = _doctorRepository.GetAll();

            IEnumerable<Doctor> filteredDoctors = doctors.Where(p => p.Name.StartsWith(filter.searchText ?? String.Empty, StringComparison.InvariantCultureIgnoreCase))
                .Skip((filter.page - 1) * filter.limit)
                .Take(filter.limit);

            var result = new PaginationResponseModel<Doctor>();

            result.Items = filteredDoctors;

            return result;
        }
    }
}

