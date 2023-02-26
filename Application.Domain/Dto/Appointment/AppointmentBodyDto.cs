using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Domain.Dto
{
	public class AppointmentBodyDto
	{
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

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int EventId { get; set; }
	}
}

