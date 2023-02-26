using Application.Domain.Dao;
using Application.Domain.Dto;
using AutoMapper;

namespace Application.API.Configuration
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Doctor, DoctorDto>();
            CreateMap<Doctor, DoctorBodyDto>();
            CreateMap<DoctorDto, Doctor>();
            CreateMap<DoctorBodyDto, Doctor>();
            CreateMap<Doctor, DoctorAppointmentsDto>()
                .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments.Select(app => new AppointmentPatientDto
                {
                    AppointmentId = app.AppointmentId,
                    Name  = app.Name,
                    Description = app.Description,
                    NotifyByEmail = app.NotifyByEmail,
                    NotifyBySMS = app.NotifyBySMS,
                    Attended = app.Attended,
                    RemindBefore = app.RemindBefore,
                    Patient = new PatientDto
                    {
                        PatientId = app.Patient.PatientId,
                        Name = app.Patient.Name
                    },
                    Event = new EventDto
                    {
                        EventId = app.Event.EventId,
                        StartTime = app.Event.StartTime,
                        EndTime = app.Event.EndTime
                    }
                })));
            CreateMap<Patient, PatientDto>();
            CreateMap<Patient, PatientBodyDto>();
            CreateMap<PatientBodyDto, Patient>();
            CreateMap<PatientDto, Patient>();
            CreateMap<Patient, PatientAppointmentsDto>()
                .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments.Select(app => new AppointmentDoctorDto
                {
                    AppointmentId = app.AppointmentId,
                    Name = app.Name,
                    Description = app.Description,
                    NotifyByEmail = app.NotifyByEmail,
                    NotifyBySMS = app.NotifyBySMS,
                    Attended = app.Attended,
                    RemindBefore = app.RemindBefore,
                    Doctor = new DoctorDto
                    {
                        DoctorId = app.Doctor.DoctorId,
                        Name = app.Doctor.Name,
                        Specialty = app.Doctor.Specialty
                    },
                    Event = new EventDto
                    {
                        EventId = app.Event.EventId,
                        StartTime = app.Event.StartTime,
                        EndTime = app.Event.EndTime
                    }
                }))); ;
            CreateMap<Event, EventDto>();
            CreateMap<Event, EventBodyDto>();
            CreateMap<EventDto, Event>();
            CreateMap<EventBodyDto, Event>();
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<Appointment, AppointmentBodyDto>();
            CreateMap<AppointmentBodyDto, Appointment>();
            CreateMap<AppointmentDto, Appointment>();
            CreateMap<Appointment, AppointmentDetailsDto>();
        }
    }
}

