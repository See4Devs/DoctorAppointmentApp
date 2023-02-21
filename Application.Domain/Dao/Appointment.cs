using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Domain.Dao
{
	public class Appointment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AppointmentId { get; set; }

		[MaxLength(150)]
		[MinLength(5)]
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; }

		[MaxLength(300)]
		public string Description { get; set; }

		[DefaultValue("true")]
		public bool NotifyByEmail { get; set; }

		[DefaultValue("false")]
		public bool NotifyBySMS { get; set; }

		[DefaultValue("false")]
		public bool Attended { get; set; }

		[DefaultValue(1)]
		public int RemindBefore { get; set; }

		public int DoctorId { get; set; }
		public Doctor Doctor { get; set; }

		public int EventId { get; set; }
		public Event Event { get; set; }

		public int PatientId { get; set; }
		public Patient Patient { get; set; }
	}
}

