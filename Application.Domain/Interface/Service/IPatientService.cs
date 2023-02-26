using Application.Domain.Dto;

namespace Application.Domain.Interfaces
{
    public interface IPatientService
    {
        Task<PaginationResponseDto<PatientDto>> GetPatientsAsync(FilterDto filter);
    }
}
