using System;
namespace Application.Domain.Dto
{
	public class PaginationResponseModel<T> where T : class
	{
        public IEnumerable<T> Items { get; set; }
    }
}

