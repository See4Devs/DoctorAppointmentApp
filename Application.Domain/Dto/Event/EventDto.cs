using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Domain.Dto
{
	public class EventDto
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int EventId { get; set; }

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

