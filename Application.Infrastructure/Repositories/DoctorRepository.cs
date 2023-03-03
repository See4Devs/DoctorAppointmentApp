using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using Application.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        readonly RepositoryContext _repositoryContext;

        public DoctorRepository(RepositoryContext context)
        {
            _repositoryContext = context;
        }


        public async Task<string> AddAsync(Doctor entity)
        {
            _repositoryContext.Doctor.Add(entity);
            await _repositoryContext.SaveChangesAsync();
            return "Successfully Created";
        }

        public async Task<string> DeleteAsync(Doctor entity)
        {
            _repositoryContext.Doctor.Remove(entity);
            await _repositoryContext.SaveChangesAsync();
            return "Successfully Deleted";
        }

        public async Task<Doctor> GetAsync(int id)
        {
            var doctor = await _repositoryContext.Doctor.FirstOrDefaultAsync(e => e.DoctorId == id);
            return doctor;
        }

        public async Task<Doctor> GetDoctorAppointmentsAsync(DateFilterDto filter, int doctorId)
        {
            DateTime startTime = filter.StartTime;
            DateTime endTime = filter.EndTime;

            //if none is specified get appointments for 1 week
            if (startTime == DateTime.MinValue)
            {
                startTime = DateTime.Today;
            }

            if(endTime == DateTime.MinValue)
            {
                endTime = DateTime.Today.AddDays(7);
            }

            var doctor = await _repositoryContext.Doctor.Include(d => d.Appointments).Select(b => new Doctor
            {
                DoctorId = b.DoctorId,
                Name = b.Name,
                Specialty = b.Specialty,
                Appointments = b.Appointments.Select(a => new Appointment
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
                    Patient = new Patient
                    {
                        PatientId = a.Patient.PatientId,
                        Name = a.Patient.Name,
                        Email = a.Patient.Email
                    },
                    Event = new Event
                    {
                        EventId = a.Event.EventId,
                        Name = a.Event.Name,
                        StartTime = a.Event.StartTime,
                        EndTime = a.Event.EndTime
                    }
                }).Where(x => x.Event.StartTime >= filter.StartTime && x.Event.EndTime <= filter.EndTime).ToList()
            }).FirstOrDefaultAsync(e => e.DoctorId == doctorId);
            return doctor;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _repositoryContext.Doctor.ToListAsync();
        }

        public async Task<string> UpdateAsync(Doctor dbEntity, Doctor entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.Specialty = entity.Specialty;
            await _repositoryContext.SaveChangesAsync();

            return "Successfully Updated";
        }
    }
}


