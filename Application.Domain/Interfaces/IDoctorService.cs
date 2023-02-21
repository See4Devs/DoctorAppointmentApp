using Application.Domain.Dao;
using Application.Domain.Dto;

namespace Application.Domain.Interfaces
{
    public interface IDoctorService
    {
        PaginationResponseModel<Doctor> GetDoctors(FilterModel filter);
    }
}

