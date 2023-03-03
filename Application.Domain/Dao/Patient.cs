using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Application.Domain.Dao
{
	public class Patient
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PatientId { get; set; }

		[Required]
		[MaxLength(150)]
		[MinLength(5)]
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[MaxLength(500)]
		public string Address { get; set; }

		public DateTime DateOfBirth { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public ICollection<Appointment> Appointments { get; set;}
	}
}

