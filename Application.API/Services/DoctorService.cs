using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;

namespace Application.API.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<PaginationResponseDto<DoctorDto>> GetDoctorsAsync(FilterDto filter)
        {
            IEnumerable<Doctor> doctors = await _doctorRepository.GetAllAsync();

            IEnumerable<DoctorDto> filteredDoctors = doctors.Where(p => p.Name.StartsWith(filter.searchText ?? String.Empty, StringComparison.InvariantCultureIgnoreCase))
                .Skip((filter.page - 1) * filter.limit)
                .Take(filter.limit).Select(b => new DoctorDto
                {
                    DoctorId = b.DoctorId,
                    Name = b.Name,
                    Specialty = b.Specialty
                });

            var result = new PaginationResponseDto<DoctorDto>();

            result.Items = filteredDoctors;

            return result;
        }
    }
}

