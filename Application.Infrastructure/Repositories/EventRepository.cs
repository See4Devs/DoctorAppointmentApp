using Application.Domain.Dao;
using Application.Domain.Interfaces;
using Application.Infrastructure.Context;

namespace Application.Infrastructure.Repositories
{
    public class EventRepository : IDataRepository<Event>
    {

        readonly RepositoryContext _repositoryContext;

        public EventRepository(RepositoryContext context)
        {
            _repositoryContext = context;
        }


        public void Add(Event entity)
        {
            _repositoryContext.Event.Add(entity);
            _repositoryContext.SaveChanges();
        }

        public void Delete(Event entity)
        {
            _repositoryContext.Event.Remove(entity);
            _repositoryContext.SaveChanges();
        }

        public Event Get(int id)
        {
            return _repositoryContext.Event
                  .FirstOrDefault(e => e.EventId == id);
        }

        public IEnumerable<Event> GetAll()
        {
            return _repositoryContext.Event.ToList();
        }

        public void Update(Event dbEntity, Event entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.Description = entity.Description;
            dbEntity.StartTime = entity.StartTime;
            dbEntity.EndTime = entity.EndTime;
            _repositoryContext.SaveChanges();
        }
    }
}


