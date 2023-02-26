using Application.Domain.Dto;

namespace Application.Domain.Interfaces
{
    public interface IDoctorService
    {
        Task<PaginationResponseDto<DoctorDto>> GetDoctorsAsync(FilterDto filter);
    }
}

