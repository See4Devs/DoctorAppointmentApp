using Application.Domain.Dao;
using Application.Domain.Dto;

namespace Application.Domain.Interfaces
{
	public interface IDoctorRepository : IDataRepository<Doctor>
	{
		Task<Doctor> GetDoctorAppointmentsAsync(DateFilterDto filter, int doctorId);
	}
}

