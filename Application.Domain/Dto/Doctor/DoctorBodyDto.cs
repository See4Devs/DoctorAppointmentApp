using System.ComponentModel.DataAnnotations;

namespace Application.Domain.Dto
{
	public class DoctorBodyDto
	{
		[Required]
		[MaxLength(150)]
		[MinLength(5)]
		public string Name { get; set; }

		[Required]
		[MaxLength(150)]
		[MinLength(5)]
		public string Specialty { get; set; }
	}
}
