using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;

namespace Application.API.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IDataRepository<Appointment> _appointmentRepository;

        public AppointmentService(IDataRepository<Appointment> appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public PaginationResponseModel<Appointment> GetAppointments(FilterModel filter)
        {
            IEnumerable<Appointment> appointments = _appointmentRepository.GetAll();

            IEnumerable<Appointment> filteredAppointments = appointments.Where(p => p.Name.StartsWith(filter.searchText ?? String.Empty, StringComparison.InvariantCultureIgnoreCase))
                .Skip((filter.page - 1) * filter.limit)
                .Take(filter.limit);

            var result = new PaginationResponseModel<Appointment>();

            result.Items = filteredAppointments;

            return result;
        }
    }
}

