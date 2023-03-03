using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Domain.Dto
{
	public class AppointmentDetailsDto
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int AppointmentId { get; set; }

		[MaxLength(150)]
		[MinLength(5)]
		public string Name { get; set; }

		[MaxLength(300)]
		public string Description { get; set; }

		public bool NotifyByEmail { get; set; }
		public bool NotifyBySMS { get; set; }
		public bool Attended { get; set; }
		public int RemindBefore { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int DoctorId { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int PatientId { get; set; }

		[Required]
		public EventDto Event { get; set; }
		public PatientDto Patient { get; set; }
		public DoctorDto Doctor { get; set; }
	}
}

