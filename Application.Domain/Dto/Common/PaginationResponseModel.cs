namespace Application.Domain.Dto
{
	public class PaginationResponseDto<T> where T : class
	{
        public IEnumerable<T> Items { get; set; }
    }
}

