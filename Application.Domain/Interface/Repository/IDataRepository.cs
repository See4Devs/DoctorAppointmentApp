namespace Application.Domain.Interfaces
{
	public interface IDataRepository<TEntity>
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<TEntity> GetAsync(int id);
		Task<string> AddAsync(TEntity entity);
		Task<string> UpdateAsync(TEntity dbEntity, TEntity entity);
		Task<string> DeleteAsync(TEntity entity);
	}
}