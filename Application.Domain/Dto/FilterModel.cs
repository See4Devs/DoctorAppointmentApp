using System;
using System.ComponentModel;

namespace Application.Domain.Dto
{
	public class FilterModel
	{
		[DefaultValue("")]
		public string searchText { get; set; }

		[DefaultValue(1)]
		public int page { get; set; }

		[DefaultValue(1)]
		public int limit { get; set; }
	}
}

