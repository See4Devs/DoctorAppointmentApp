using System;
using Application.Domain.Dao;
using Application.Domain.Dto;

namespace Application.Domain.Interfaces
{
    public interface IPatientService
    {
        PaginationResponseModel<Patient> GetPatients(FilterModel filter);
    }
}
