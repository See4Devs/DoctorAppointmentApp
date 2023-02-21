using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Domain.Dao;

namespace Application.Domain.Dto
{
	public class EventModel
	{
		[MaxLength(150)]
		[MinLength(5)]
		[Column(TypeName = "nvarchar(50)")]
		public string Name { get; set; }

		[MinLength(5)]
		public string Description { get; set; }

		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public Event TransformToDto()
		{
			Event newObject = new Event();
			newObject.Name = this.Name;
			newObject.Description = this.Description;
			newObject.StartTime = this.StartTime;
			newObject.EndTime = this.EndTime;
			return newObject;
		}
	}
}

