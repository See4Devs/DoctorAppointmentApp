using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;

namespace Application.API.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<PaginationResponseDto<AppointmentDto>> GetAppointmentsAsync(FilterDto filter)
        {
            IEnumerable<Appointment> appointments = await _appointmentRepository.GetAllAsync();

            IEnumerable<AppointmentDto> filteredAppointments = appointments.Where(p => p.Name.StartsWith(filter.searchText ?? String.Empty, StringComparison.InvariantCultureIgnoreCase))
                .Skip((filter.page - 1) * filter.limit)
                .Take(filter.limit).Select(a => new AppointmentDto
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
                    Event = new EventDto
                    {
                        EventId = a.EventId,
                        StartTime = a.Event.StartTime,
                        EndTime = a.Event.EndTime
                    }
                });

            var result = new PaginationResponseDto<AppointmentDto>();

            result.Items = filteredAppointments;

            return result;
        }
    }
}

