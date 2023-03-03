using Application.Domain.Dao;
using Application.Domain.Dto;

namespace Application.Domain.Interfaces
{
	public interface IPatientRepository : IDataRepository<Patient>
	{
		Task<Patient> GetPatientAppointmentsAsync(DateFilterDto filter, int patientId);
	}
}

