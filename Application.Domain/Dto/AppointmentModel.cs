using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Domain.Dao;

namespace Application.Domain.Dto
{
	public class AppointmentModel
	{

		[MaxLength(150)]
		[MinLength(5)]
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; }

		[MaxLength(300)]
		public string Description { get; set; }

		public bool NotifyByEmail { get; set; }
		public bool NotifyBySMS { get; set; }
		public bool Attended { get; set; }

		[DefaultValue(1)]
		public int RemindBefore { get; set; }

		[Required]
		public int DoctorId { get; set; }

		[Required]
		public int EventId { get; set; }

		[Required]
		public int PatientId { get; set; }

		public Appointment TransformToDto()
		{
			Appointment newObject = new Appointment();
			newObject.Name = this.Name;
			newObject.Description = this.Description;
			newObject.NotifyBySMS = this.NotifyBySMS;
			newObject.RemindBefore = this.RemindBefore;
			newObject.Attended = this.Attended;
			newObject.DoctorId = this.DoctorId;
			newObject.EventId = this.EventId;
			newObject.PatientId = this.PatientId;

			return newObject;
		}
	}
}

