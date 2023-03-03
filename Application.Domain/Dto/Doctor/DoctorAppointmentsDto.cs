using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Domain.Dto
{
	public class DoctorAppointmentsDto
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int DoctorId { get; set; }

		[Required]
		[MaxLength(150)]
		[MinLength(5)]
		public string Name { get; set; }

		[Required]
		[MaxLength(150)]
		[MinLength(5)]
		public string Specialty { get; set; }

		public List<AppointmentPatientDto> Appointments { get; set; }
	}
}

