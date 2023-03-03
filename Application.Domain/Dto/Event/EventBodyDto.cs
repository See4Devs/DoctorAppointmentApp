using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Domain.Dto
{
	public class EventBodyDto
	{
		[MaxLength(150)]
		[MinLength(5)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public string Name { get; set; }

		[MinLength(5)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public string Description { get; set; }

		[Required]
		public DateTime StartTime { get; set; }

		[Required]
		public DateTime EndTime { get; set; }
	}
}

