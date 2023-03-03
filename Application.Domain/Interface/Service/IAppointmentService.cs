using Application.Domain.Dto;

namespace Application.Domain.Interfaces
{
    public interface IAppointmentService
    {
        Task<PaginationResponseDto<AppointmentDto>> GetAppointmentsAsync(FilterDto filter);
    }
}

