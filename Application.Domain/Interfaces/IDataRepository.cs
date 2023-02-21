using System;
namespace Application.Domain.Interfaces
{
	public interface IDataRepository<TEntity>
	{
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
    }
}

