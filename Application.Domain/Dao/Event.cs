using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Domain.Dao
{
	public class Event
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int EventId { get; set; }

		[MaxLength(150)]
		[MinLength(5)]
		[Column(TypeName = "nvarchar(50)")]
		public string Name { get; set; }

		[MinLength(5)]
		public string Description { get; set; }

		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public List<Appointment> Appointments { get; set; }
	}
}

