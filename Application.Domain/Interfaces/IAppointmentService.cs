using Application.Domain.Dao;
using Application.Domain.Dto;

namespace Application.Domain.Interfaces
{
    public interface IAppointmentService
    {
        PaginationResponseModel<Appointment> GetAppointments(FilterModel filter);
    }
}

