using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Domain.Dto
{
	public class PatientDto
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int PatientId { get; set; }

		[Required]
		[MaxLength(150)]
		[MinLength(5)]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public string Email { get; set; }

		[MaxLength(500)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public string Address { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public DateTime DateOfBirth { get; set; }
	}
}

