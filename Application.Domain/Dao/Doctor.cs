using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Domain.Dao
{
    public class Doctor
	{
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int DoctorId { get; set; }

		[Required]
		[MaxLength(150)]
		[MinLength(5)]
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; }

		[Required]
		[MaxLength(150)]
		[MinLength(5)]
		[Column(TypeName = "nvarchar(100)")]
		public string Specialty { get; set; }

		public List<Appointment> Appointments { get; set; }
	}
}

