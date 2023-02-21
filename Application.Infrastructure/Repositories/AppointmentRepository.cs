using Application.Domain.Dao;
using Application.Domain.Interfaces;
using Application.Infrastructure.Context;

namespace Application.Infrastructure.Repositories
{
    public class AppointmentRepository : IDataRepository<Appointment>
    {

        readonly RepositoryContext _repositoryContext;

        public AppointmentRepository(RepositoryContext context)
        {
            _repositoryContext = context;
        }


        public void Add(Appointment entity)
        {
            _repositoryContext.Appointment.Add(entity);
            _repositoryContext.SaveChanges();
        }

        public void Delete(Appointment entity)
        {
            _repositoryContext.Appointment.Remove(entity);
            _repositoryContext.SaveChanges();
        }

        public Appointment Get(int id)
        {
            var result = _repositoryContext.Appointment
                  .FirstOrDefault(e => e.AppointmentId == id);


            //Not my best work here, had some issues, but it will do the job
            var DoctorId = result?.DoctorId;
            var EventId = result?.EventId;
            var PatientId = result?.PatientId;

            var Doctor = _repositoryContext.Doctor
                  .FirstOrDefault(e => e.DoctorId == DoctorId);

            var Event = _repositoryContext.Event
                  .FirstOrDefault(e => e.EventId == EventId);

            var Patient = _repositoryContext.Patient
                  .FirstOrDefault(e => e.PatientId == PatientId);

            if(result != null)
            {
                if(Patient != null)
                {
                    result.Patient = new Patient
                    {
                        Address = Patient.Address,
                        Name = Patient.Name,
                        Email = Patient.Email,
                        DateOfBirth = Patient.DateOfBirth,
                        PatientId = Patient.PatientId
                    };
                }
                if (Doctor != null)
                {
                    result.Doctor = new Doctor
                    {
                        DoctorId = Doctor.DoctorId,
                        Name = Doctor.Name,
                        Specialty = Doctor.Specialty
                    };
                }
                if (Event != null)
                {
                    result.Event = new Event
                    {
                        EventId = Event.EventId,
                        Name = Event.Name,
                        Description = Event.Description,
                        StartTime = Event.StartTime,
                        EndTime = Event.EndTime
                    };
                }
            }

            return result;
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _repositoryContext.Appointment.ToList();
        }

        public void Update(Appointment dbEntity, Appointment entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.Attended = entity.Attended;
            dbEntity.NotifyByEmail = entity.NotifyByEmail;
            dbEntity.NotifyBySMS = entity.NotifyBySMS;
            dbEntity.RemindBefore = entity.RemindBefore;
            dbEntity.PatientId = entity.PatientId;
            dbEntity.EventId = entity.EventId;
            dbEntity.DoctorId = entity.DoctorId;

            _repositoryContext.SaveChanges();
        }
    }
}