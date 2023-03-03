using System.ComponentModel;

namespace Application.Domain.Dto
{
	public class FilterDto
	{
		[DefaultValue("")]
		public string searchText { get; set; }

		[DefaultValue(1)]
		public int page { get; set; }

		[DefaultValue(1)]
		public int limit { get; set; }
	}
}

