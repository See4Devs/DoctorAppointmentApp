using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using Application.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {

        readonly RepositoryContext _repositoryContext;

        public PatientRepository(RepositoryContext context)
        {
            _repositoryContext = context;
        }

        public async Task<string> AddAsync(Patient entity)
        {
            await _repositoryContext.Patient.AddAsync(entity);
            await _repositoryContext.SaveChangesAsync();
            return "Successfully Created";
        }

        public async Task<string> DeleteAsync(Patient entity)
        {
            _repositoryContext.Patient.Remove(entity);
            await _repositoryContext.SaveChangesAsync();
            return "Successfully Deleted";
        }

        public async Task<Patient> GetAsync(int id)
        {
            var result = await _repositoryContext.Patient
                  .FirstOrDefaultAsync(e => e.PatientId == id);
            return result;
        }

        public async Task<Patient> GetPatientAppointmentsAsync(DateFilterDto filter, int patientId)
        {
            DateTime startTime = filter.StartTime;
            DateTime endTime = filter.EndTime;

            //if none is specified get appointments for 1 week
            if (startTime == DateTime.MinValue)
            {
                startTime = DateTime.Today;
            }

            if (endTime == DateTime.MinValue)
            {
                endTime = DateTime.Today.AddDays(7);
            }

            var result = await _repositoryContext.Patient.Include(d => d.Appointments).Select( b => new Patient
            {
                PatientId = b.PatientId,
                Name = b.Name,
                Email = b.Email,
                Address = b.Address,
                DateOfBirth = b.DateOfBirth,
                Appointments = b.Appointments.Select( a => new Appointment
                {
                    AppointmentId = a.AppointmentId,
                    Attended = a.Attended,
                    Name = a.Name,
                    NotifyByEmail = a.NotifyByEmail,
                    Description = a.Description,
                    DoctorId = a.DoctorId,
                    PatientId = a.PatientId,
                    EventId = a.EventId,
                    NotifyBySMS = a.NotifyBySMS,
                    RemindBefore = a.RemindBefore,
                    Event = new Event
                    {
                        EventId = a.Event.EventId,
                        Name = a.Event.Name,
                        StartTime = a.Event.StartTime,
                        EndTime = a.Event.EndTime
                    },
                    Doctor = new Doctor
                    {
                        DoctorId = a.Doctor.DoctorId,
                        Name = a.Doctor.Name,
                        Specialty = a.Doctor.Specialty
                    }
                }).Where(x => x.Event.StartTime >= filter.StartTime && x.Event.EndTime <= filter.EndTime).ToList()
            })
                  .FirstOrDefaultAsync(e => e.PatientId == patientId);
            return result;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _repositoryContext.Patient.ToListAsync();
        }

        public async Task<string> UpdateAsync(Patient dbEntity, Patient entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.Address = entity.Address;
            dbEntity.Email = entity.Email;
            dbEntity.DateOfBirth = entity.DateOfBirth;

            await _repositoryContext.SaveChangesAsync();

            return "Successfully Updated";
        }
    }
}