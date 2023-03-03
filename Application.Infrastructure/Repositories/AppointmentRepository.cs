using Application.Domain.Dao;
using Application.Domain.Interfaces;
using Application.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {

        readonly RepositoryContext _repositoryContext;

        public AppointmentRepository(RepositoryContext context)
        {
            _repositoryContext = context;
        }


        public async Task<string> AddAsync(Appointment entity)
        {
            await _repositoryContext.Appointment.AddAsync(entity);
            await _repositoryContext.SaveChangesAsync();
            return "Successfully Created";
        }

        public async Task<string> DeleteAsync(Appointment entity)
        {
            _repositoryContext.Appointment.Remove(entity);
            await _repositoryContext.SaveChangesAsync();
            return "Successfully Deleted";
        }

        public async Task<Appointment> GetAsync(int id)
        {
            var result = await _repositoryContext.Appointment.Include(e => e.Event).Include(p => p.Patient).Include(d => d.Doctor).Select(a => new Appointment
            {
                AppointmentId = a.AppointmentId,
                Name = a.Name,
                Description = a.Description,
                NotifyByEmail = a.NotifyByEmail,
                NotifyBySMS = a.NotifyBySMS,
                Attended = a.Attended,
                RemindBefore = a.RemindBefore,
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                Event = new Event
                {
                    EventId = a.EventId,
                    StartTime = a.Event.StartTime,
                    EndTime = a.Event.EndTime
                },
                Doctor = new Doctor
                {
                    DoctorId = a.Doctor.DoctorId,
                    Name = a.Doctor.Name,
                    Specialty = a.Doctor.Specialty
                },
                Patient = new Patient
                {
                    PatientId = a.Patient.PatientId,
                    Name = a.Patient.Name
                }
            }).FirstOrDefaultAsync(e => e.AppointmentId == id);
            return result;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            var result = await _repositoryContext.Appointment.Include(e => e.Event).Include(p => p.Patient).Include(d => d.Doctor).Select(a => new Appointment
            {
                AppointmentId = a.AppointmentId,
                Name = a.Name,
                Description = a.Description,
                NotifyByEmail = a.NotifyByEmail,
                NotifyBySMS = a.NotifyBySMS,
                Attended = a.Attended,
                RemindBefore = a.RemindBefore,
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                Event = new Event
                {
                    EventId = a.EventId,
                    StartTime = a.Event.StartTime,
                    EndTime = a.Event.EndTime
                },
                Doctor = new Doctor
                {
                    DoctorId = a.Doctor.DoctorId,
                    Name = a.Doctor.Name,
                    Specialty = a.Doctor.Specialty
                },
                Patient = new Patient
                {
                    PatientId = a.Patient.PatientId,
                    Name = a.Patient.Name
                }
            }).ToListAsync();

            return result;
        }

        public async Task<string> UpdateAsync(Appointment dbEntity, Appointment entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.Attended = entity.Attended;
            dbEntity.NotifyByEmail = entity.NotifyByEmail;
            dbEntity.NotifyBySMS = entity.NotifyBySMS;
            dbEntity.RemindBefore = entity.RemindBefore;
            dbEntity.PatientId = entity.PatientId;
            dbEntity.EventId = entity.EventId;
            dbEntity.DoctorId = entity.DoctorId;
            await _repositoryContext.SaveChangesAsync();

            return "Successfully Updated";
        }
    }
}